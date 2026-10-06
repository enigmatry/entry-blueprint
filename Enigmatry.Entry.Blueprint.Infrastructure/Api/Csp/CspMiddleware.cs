using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;

/// <summary>
/// Sets the Content-Security-Policy header on every response: a strict policy for API endpoints and the
/// configured nonce-based policies for Swagger UI and for the Angular application.
/// </summary>
[UsedImplicitly]
public class CspMiddleware(RequestDelegate next, IConfiguration configuration)
{
    private const string StrictCsp = "default-src 'none'; frame-ancestors 'none'";

    [UsedImplicitly]
    public Task InvokeAsync(HttpContext context, NonceProvider nonceProvider)
    {
        context.Response.Headers[CspConstants.HeaderName] = GetCspValue(context.Request.Path, nonceProvider.Nonce);
        return next(context);
    }

    private string GetCspValue(PathString path, string nonce)
    {
        if (path.StartsWithSegments("/swagger", StringComparison.OrdinalIgnoreCase))
        {
            return GetConfiguredCsp(CspConstants.SettingsName, nonce) ?? StrictCsp;
        }

        if (path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase))
        {
            return StrictCsp;
        }

        return GetConfiguredCsp(CspConstants.SpaSettingsName, nonce) ?? StrictCsp;
    }

    // An empty value (e.g. a Web Deploy parameter left at its empty default) counts as missing, so the strict
    // policy is sent instead of an empty header that would disable CSP altogether.
    private string? GetConfiguredCsp(string settingsName, string nonce)
    {
        var value = configuration.GetValue<string>(settingsName);
        return String.IsNullOrWhiteSpace(value)
            ? null
            : value.Replace(CspConstants.NoncePlaceholder, nonce, StringComparison.Ordinal);
    }
}
