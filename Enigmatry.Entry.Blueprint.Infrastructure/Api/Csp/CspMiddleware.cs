using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;

[UsedImplicitly]
public class CspMiddleware
{
    private const string StrictCsp = "default-src 'none'; frame-ancestors 'none'";

    private readonly RequestDelegate _next;
    private readonly string? _spaCsp;

    public CspMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _spaCsp = ReadSpaPolicy(configuration);
    }

    [UsedImplicitly]
    public Task InvokeAsync(HttpContext context, NonceProvider nonceProvider)
    {
        // Swagger UI only runs in local development and relies on inline scripts and styles.
        if (context.Request.Path.StartsWithSegments("/swagger", StringComparison.OrdinalIgnoreCase))
        {
            return _next(context);
        }

        var csp = GetCspValue(context.Request.Path, nonceProvider.Nonce);
        context.Response.Headers[CspConstants.HeaderName] = csp;
        // The exception handler clears the response (headers included) before writing ProblemDetails; put it back.
        context.Response.OnStarting(() =>
        {
            if (!context.Response.Headers.ContainsKey(CspConstants.HeaderName))
            {
                context.Response.Headers[CspConstants.HeaderName] = csp;
            }

            return Task.CompletedTask;
        });
        return _next(context);
    }

    private string GetCspValue(PathString path, string nonce)
    {
        if (path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase) || _spaCsp == null)
        {
            return StrictCsp;
        }

        return _spaCsp.Replace(CspConstants.NoncePlaceholder, nonce, StringComparison.Ordinal);
    }

    // An empty value (e.g. an unset Web Deploy parameter) counts as missing and falls back to the strict policy;
    // a policy without the placeholder can never carry the nonce, so that misconfiguration fails at startup.
    private static string? ReadSpaPolicy(IConfiguration configuration)
    {
        var value = configuration.GetValue<string>(CspConstants.SpaSettingsName);
        if (String.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (!value.Contains(CspConstants.NoncePlaceholder, StringComparison.Ordinal))
        {
            throw new InvalidOperationException(
                $"Configuration value '{CspConstants.SpaSettingsName}' must contain the nonce placeholder '{CspConstants.NoncePlaceholder}'.");
        }

        return value;
    }
}
