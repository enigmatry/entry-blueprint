# CSP hardening with per-request nonces (BP-1601)

Jira: https://enigmatry.atlassian.net/browse/BP-1601

## Problem

The Content-Security-Policy (CSP) that protects the Angular app is a static IIS header in
`Enigmatry.Entry.Blueprint.Api/web.config`. It allows `'unsafe-inline'` for both `script-src` and
`style-src` and lacks `base-uri 'self'`. The ticket names `enigmatry-entry-blueprint-app/web.config`
as the location; that file does not exist. The API serves the built Angular app from its `wwwroot`
(the Angular `dist/browser` output is published there), so the API's web.config is the one that
applies to the SPA.

Angular injects component styles as `<style>` elements at runtime. Dropping `'unsafe-inline'` from
`style-src` therefore requires a per-request nonce that Angular can stamp onto those elements
(via the `ngCspNonce` attribute on the root element). A static header cannot carry a per-request
nonce, so CSP generation moves from IIS into the ASP.NET Core pipeline.

The design is a port of the implementation in the Windesheim Student Result System (WSR-5466),
which in turn follows the entry-blueprint hosting pattern. NHL Stenden Study Coach Monitor and
Q-Park Parkingshop use the same approach.

## Goals

- `script-src` and `style-src` no longer contain `'unsafe-inline'`.
- `base-uri 'self'` and `form-action 'self'` are present.
- Every response produced by the application carries a CSP header. API responses get a strict
  policy; SPA and Swagger responses get the nonce-based policy.
- The raw `index.html` with the unsubstituted placeholder is never served.
- The deployment profiles keep overriding the SPA policy per environment.
- Swagger UI keeps working under the nonce-based policy.

## Non-goals

- Changing the Angular build (`--nonce`, `inlineCritical`, hashes). `inlineCritical` is already
  `false`, so the built `index.html` contains no inline styles or scripts.
- Changing other security headers (`X-Frame-Options`, `Permissions-Policy`, `Referrer-Policy`,
  HSTS). `frame-ancestors 'none'` and `X-Frame-Options: DENY` stay as they are.
- Subresource integrity for the Google Fonts stylesheet.
- The Scheduler project (it serves no HTML).

## Design

### 1. Nonce generation

`NonceProvider` (scoped service) generates 32 cryptographically random bytes per request and
exposes them base64-encoded as `Nonce`. Registered with `services.AddScoped<NonceProvider>()` in
`ProgramExtensions.AppAddServices`.

`CspConstants` holds the header name, the two configuration keys and the placeholder:

| Constant | Value |
|---|---|
| `HeaderName` | `Content-Security-Policy` |
| `SwaggerSettingsName` | `ContentSecurityPolicyValue` (used for `/swagger`) |
| `SpaSettingsName` | `SpaContentSecurityPolicyValue` (used for the SPA) |
| `NoncePlaceholder` | `**PLACEHOLDER_NONCE_SERVER**` |

Both classes live in `Enigmatry.Entry.Blueprint.Infrastructure/Api/Csp/`.

### 2. CSP middleware

`CspMiddleware` (in `Infrastructure/Api/Csp/`) sets the header on every response before the
next middleware runs:

| Request path | Policy |
|---|---|
| starts with `/swagger` | `ContentSecurityPolicyValue` with placeholder replaced; falls back to the built-in default when not configured |
| starts with `/api` | built-in default `default-src 'none'; frame-ancestors 'none'` |
| anything else (SPA, health check) | `SpaContentSecurityPolicyValue` with placeholder replaced; falls back to the built-in default when not configured |

Placeholder replacement uses `StringComparison.Ordinal`. The middleware is registered with
`app.UseMiddleware<CspMiddleware>()` as the first middleware in `AppConfigureWebApplication`,
before the static-file middleware, so static assets carry the header too.

### 3. Swagger nonce middleware

`SwaggerNonceMiddleware` (in `Infrastructure/Api/Csp/`) buffers responses for paths starting
with `/swagger`. When the response is `text/html` it adds `nonce="…"` to every `<script>` and
`<style>` tag that does not already have one and clears `Content-Length`. Non-HTML responses are
copied through unchanged. It is registered after `UseRouting()` and before the Swagger middleware.
Swagger is only enabled in Development in this repo, but downstream projects enable it on test
environments, so the middleware is part of the template.

### 4. Serving the SPA

`SpaStartupExtensions` (in `Infrastructure/Api/Startup/`) replaces the current
`UseDefaultFiles` / `UseStaticFiles` / `MapFallbackToFile("index.html")` trio.

`AppUseSpaStaticFiles(this WebApplication app)`:

- Adds an inline middleware that rewrites a request path equal to `/index.html`
  (case-insensitive) to `/`, so direct navigation to `/index.html` reaches the fallback.
- Calls `UseStaticFiles` with an `IndexHtmlHidingFileProvider` wrapping
  `app.Environment.WebRootFileProvider`. The wrapper returns `NotFoundFileInfo` for any subpath
  that, after trimming leading `/` and `\`, equals `index.html`. This closes the `//index.html`
  hole where `PhysicalFileProvider` would otherwise serve the raw file.
- `OnPrepareResponse` sets `Cache-Control: public, max-age=604800, immutable` on files whose
  name matches the Angular content hash pattern `-[A-Z0-9]{8}\.[a-z0-9]+$`. Other static files
  (favicon, `assets/**`) get no long-lived cache header. This also fixes the existing typo
  `max-age: 604800` (colon instead of `=`), which browsers ignore today.

`AppMapSpaFallback(this WebApplication app)` maps a fallback endpoint (`AllowAnonymous`) that:

- Returns 404 when the path starts with `/api` or `/swagger`, or when the path has a file
  extension (asset requests must not get the SPA shell).
- Returns 404 when `wwwroot/index.html` does not exist (local development without a built SPA;
  also the state inside the integration tests).
- Otherwise reads `index.html`, applies `InjectNonce`, and writes it with
  `Content-Type: text/html; charset=utf-8` and `Cache-Control: no-cache, no-store, must-revalidate`.

`InjectNonce(string html, string nonce)` (internal, unit-tested):

- Inserts `nonce="…"` into every `<link … rel="stylesheet" …>` opening tag, regardless of
  attribute order, skipping tags that already carry a `nonce` attribute. Regex:
  `<link\s(?![^>]*\bnonce=)(?=[^>]*\brel="stylesheet")`, case-insensitive, source-generated.
- Replaces every occurrence of the placeholder with the nonce.

Pipeline order in `AppConfigureWebApplication` becomes:

```
UseMiddleware<CspMiddleware>
AppUseSpaStaticFiles
UseRouting
UseMiddleware<SwaggerNonceMiddleware>
(developer exception page, CORS, HTTPS, exception handler, auth, log context — unchanged)
MapControllers().RequireAuthorization()
MapEntryHealthCheck
AppMapSpaFallback
Swagger (Development only)
```

`MapFallback` has the lowest routing priority, so the ordering of `AppMapSpaFallback` relative to
the other `Map*` calls is not load-bearing, but it is placed last for readability.

### 5. Angular changes

`enigmatry-entry-blueprint-app/src/index.html`: add `ngCspNonce="**PLACEHOLDER_NONCE_SERVER**"`
to `<app-root>`. Angular reads this attribute at bootstrap and applies the nonce to every
`<style>` element it creates, including Angular Material and CDK styles. No `CSP_NONCE` provider
is needed. Under `ng serve` the placeholder stays literal, which is harmless because the dev
server sends no CSP header.

No templates in the app use `style="…"` attributes or `[style]` bindings that set the attribute
as a string, so nothing else needs a nonce.

### 6. Policies

Built-in default (API responses, and fallback when a configured value is missing):

```
default-src 'none'; frame-ancestors 'none'
```

`ContentSecurityPolicyValue` (Swagger), committed in `appsettings.json`:

```
default-src 'none'; frame-ancestors 'none'; base-uri 'self'; form-action 'self'; img-src 'self' data:; style-src 'self' 'nonce-**PLACEHOLDER_NONCE_SERVER**'; script-src 'self' 'nonce-**PLACEHOLDER_NONCE_SERVER**'; connect-src 'self' https://login.microsoftonline.com
```

`SpaContentSecurityPolicyValue`:

- `appsettings.json`: `__spaContentSecurityPolicyValue__` (Web Deploy token).
- `appsettings.Development.json` and `Deployment/publish-web-profile-test-app.proj`:

```
default-src 'none'; base-uri 'self'; frame-ancestors 'none'; form-action 'self'; script-src 'self' 'nonce-**PLACEHOLDER_NONCE_SERVER**'; style-src 'self' 'nonce-**PLACEHOLDER_NONCE_SERVER**' fonts.googleapis.com; font-src 'self' fonts.gstatic.com; img-src 'self' data:; connect-src 'self' https://enigmatryexternaldev.ciamlogin.com https://js.monitor.azure.com https://*.in.applicationinsights.azure.com; frame-src 'self' https://enigmatryexternaldev.ciamlogin.com
```

Rationale per directive:

- `script-src 'self' 'nonce-…'`: Angular bundles are same-origin; the nonce keeps `CSP_NONCE`
  usable for scripts injected at runtime, matching the ticket's target policy.
- `style-src 'self' 'nonce-…' fonts.googleapis.com`: component styles via nonce, bundled
  `styles-*.css` via `'self'`, Material Icons stylesheet via host allow-list (the fallback also
  stamps the nonce on that `<link>`).
- `font-src 'self' fonts.gstatic.com`: Material Icons font files.
- `connect-src`: the API is same-origin; the MSAL authority receives token requests; the
  Application Insights hosts receive telemetry.
- `frame-src 'self' <authority>`: MSAL silent token renewal uses a hidden iframe to the authority.
- `frame-ancestors 'none'` + `X-Frame-Options: DENY`: unchanged, by decision.

The test deployment profile keeps its environment-specific hosts (for example
`westeurope-5.in.applicationinsights.azure.com`) and its existing `frame-ancestors 'self'`; only
`style-src` gains the nonce and loses `'unsafe-inline'`, `script-src` gains the nonce, and
`frame-src`/`connect-src` gain the test authority. The profile currently lacks the MSAL authority
entirely; adding it is part of this change and is called out in the PR description.

### 7. web.config and deployment parameters

`Enigmatry.Entry.Blueprint.Api/web.config`:

- Remove the `Content-Security-Policy` entry from `customHeaders`. Leaving it would produce two
  CSP headers and the browser would enforce both.
- Add an outbound rule that rewrites `RESPONSE_Content_Security_Policy` to
  `default-src 'none'; style-src 'self' 'unsafe-inline'; img-src 'self' data:; frame-ancestors 'none'; base-uri 'self'; form-action 'self'`
  only when the response has no CSP header (`{RESPONSE_Content_Security_Policy}` matches `^$`).
  This covers `app_offline.htm` and IIS error pages, which IIS serves without the app.
- The existing `no-cache` rule for `text/html` stays.

`Enigmatry.Entry.Blueprint.Api/parameters.xml`: replace the `Content Security Policy` XmlFile
parameter with a `SPA Content Security Policy` TextFile parameter that matches
`__spaContentSecurityPolicyValue__` in `appsettings.json`.

`Deployment/publish-web-profile-test-app.proj`: rename the `ParameterValue` to
`SPA Content Security Policy` and set the nonce-based policy.

### 8. Error handling

- Missing configuration never breaks requests: the middleware falls back to the built-in strict
  policy, which is the safe direction.
- A missing `wwwroot/index.html` yields 404 from the fallback rather than an exception.
- `SwaggerNonceMiddleware` restores the original response body stream even when the downstream
  response is not HTML.

### 9. Testing

Unit (`Enigmatry.Entry.Blueprint.Api.Tests`, `[Category("unit")]`, Shouldly):

- `SpaStartupExtensions.InjectNonce`: stylesheet link with `rel` first; with `href` first;
  non-stylesheet link unchanged; link that already has a nonce unchanged; placeholder replaced.
- `SwaggerNonceMiddleware` nonce injection: `<script>` and `<style>` tags gain a nonce; tags that
  already have one are unchanged; `<script src=…>` keeps its attributes.

Integration (`IntegrationFixtureBase`, `[Category("integration")]`):

- `GET /api/products` response carries `Content-Security-Policy: default-src 'none'; frame-ancestors 'none'`.
- `GET /api/does-not-exist` returns 404 (not the SPA shell).
- `GET /some/asset.js` returns 404.
- `GET /` returns 404 in the test host (no built SPA in `wwwroot`) and still carries a CSP header
  whose value is the configured SPA policy with a nonce substituted (the test configuration sets
  `SpaContentSecurityPolicyValue`), and the nonce differs between two requests.

Manual smoke (recorded in the PR):

- Build the Angular app, publish or run the API with `wwwroot` populated, open the app, confirm
  no CSP violations in the browser console, confirm the header nonce equals the `ngCspNonce`
  value in the served HTML, confirm `GET /index.html` and `GET //index.html` do not contain the
  placeholder, confirm hashed bundles have the immutable cache header.
- Open `/swagger` in Development and confirm it renders.

### 10. Files

New:

- `Enigmatry.Entry.Blueprint.Infrastructure/Api/Csp/CspConstants.cs`
- `Enigmatry.Entry.Blueprint.Infrastructure/Api/Csp/NonceProvider.cs`
- `Enigmatry.Entry.Blueprint.Infrastructure/Api/Csp/CspMiddleware.cs`
- `Enigmatry.Entry.Blueprint.Infrastructure/Api/Csp/SwaggerNonceMiddleware.cs`
- `Enigmatry.Entry.Blueprint.Infrastructure/Api/Startup/SpaStartupExtensions.cs`
- `Enigmatry.Entry.Blueprint.Infrastructure/Api/Startup/IndexHtmlHidingFileProvider.cs`
- `Enigmatry.Entry.Blueprint.Api.Tests/CoreFeatures/Csp/NonceProviderFixture.cs`
- `Enigmatry.Entry.Blueprint.Api.Tests/CoreFeatures/Csp/CspMiddlewareFixture.cs`
- `Enigmatry.Entry.Blueprint.Api.Tests/CoreFeatures/Csp/SwaggerNonceMiddlewareFixture.cs`
- `Enigmatry.Entry.Blueprint.Api.Tests/CoreFeatures/Csp/IndexHtmlHidingFileProviderFixture.cs`
- `Enigmatry.Entry.Blueprint.Api.Tests/CoreFeatures/Csp/SpaStartupExtensionsFixture.cs`
- `Enigmatry.Entry.Blueprint.Api.Tests/CoreFeatures/Csp/SecurityHeadersFixture.cs`

Changed:

- `Enigmatry.Entry.Blueprint.Api/ProgramExtensions.cs`
- `Enigmatry.Entry.Blueprint.Api/web.config`
- `Enigmatry.Entry.Blueprint.Api/parameters.xml`
- `Enigmatry.Entry.Blueprint.Api/appsettings.json`
- `Enigmatry.Entry.Blueprint.Api/appsettings.Development.json`
- `Enigmatry.Entry.Blueprint.Infrastructure.Tests/Configuration/TestConfigurationBuilder.cs` (the
  integration host uses only this in-memory configuration, so the SPA policy must be added here)
- `Deployment/publish-web-profile-test-app.proj`
- `enigmatry-entry-blueprint-app/src/index.html`
- `CLAUDE.md` (one bullet under Key Conventions describing where the CSP lives and the placeholder)
