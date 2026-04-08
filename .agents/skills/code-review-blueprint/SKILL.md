---
name: code-review-blueprint
description: Enigmatry Entry Blueprint code review checklist covering .NET 9 and Angular/TypeScript. Use this when reviewing or performing a code review on blueprint changes.
---

# Blueprint Code Review

## 🔴 Block merge

- **Nullable suppressed**: `!` operator used to silence a nullable warning — fix the root cause
- **Logic in controller**: business logic placed directly in a controller instead of an `IRequestHandler<T>` — move it to the handler
- **API contract changed** without regenerating the NSwag TypeScript client (`npm run nswag`)
- **Secret / credential** committed to code or config
- **Build warning introduced** — `TreatWarningsAsErrors` is on; CI will fail
- **Generated Angular file edited manually** — files under `src/app/features/` produced by `npm run codegen:run` must never be edited by hand; update the `CodeGeneration.Setup` configuration instead
- **NuGet version added to a `.csproj`** — all versions must be declared in `Directory.Packages.props` only

## 🟡 Requires discussion

- **No tests** for a new handler, validator, or domain rule
- **Service registered** via `IServiceCollection` instead of an Autofac module in `Infrastructure/Autofac/Modules/`
- **`.subscribe()`** used in Angular where `firstValueFrom()` would suffice (one-shot observable)
- **FluentValidation validator** exists but not wired to DI, or missing entirely for a command/query that has user inputs
- **Static `Log.*`** used in business code instead of injected `ILogger<T>`
- **Domain entity** exposes public setters instead of using the `Create`/`Update` factory methods
- **Non-standalone** Angular component created without justification

## 🟢 Suggestions

- **Separate `[Test]` methods** that differ only in literal values → collapse with `[TestCase]` / `[TestCaseSource]`
- **Separate `it()` blocks** that differ only in data → collapse with `it.each`
- **`nameof()`** not used where a string references a member name
- **Comment** explains *what* the code does rather than *why* — remove or rewrite
- **Explicit default argument** passed to a helper that already defaults to that value
- **Missing `[PublicAPI]`** on a DTO / request / response type, or missing `[UsedImplicitly]` on a handler or validator

## Checklist

### .NET
- [ ] All business logic in `IRequestHandler<T>` — not in controllers or plain services
- [ ] FluentValidation validator present for every command/query that has user inputs
- [ ] No `!` null-forgiving operators; no new nullable warnings
- [ ] New services registered in an Autofac module (not `IServiceCollection`)
- [ ] Serilog message templates used — no string interpolation in log calls
- [ ] Tests cover happy path + key validation/error paths
- [ ] `[TestCase]` / `[TestCaseSource]` used wherever multiple tests share the same structure
- [ ] NuGet `Version=` not added to any `.csproj` — use `Directory.Packages.props`
- [ ] `[PublicAPI]` on DTOs / request / response types; `[UsedImplicitly]` on handlers and validators
- [ ] Domain entities: private setters, `Create(Command)` factory, `Update(Command)` method

### Angular / TypeScript
- [ ] `firstValueFrom()` for single-value observables; `.subscribe()` only for true streams
- [ ] `readonly` arrow-function properties for all class methods except Angular lifecycle hooks
- [ ] `ngOnInit`, `ngOnDestroy`, etc. declared as plain methods (not arrow properties)
- [ ] `it.each` used for parameterised Jest tests instead of repeated `it()` blocks
- [ ] Generated files in `src/app/features/` NOT edited manually
- [ ] NSwag client regenerated if API contracts changed (`npm run nswag`)
- [ ] New i18n keys reuse `shared.*` for generic labels; no duplicate keys

