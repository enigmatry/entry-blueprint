using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;

[UsedImplicitly]
public class CspMiddleware
{
    private const string StrictCsp = "default-src 'none'; frame-ancestors 'none'";

    private readonly RequestDelegate _next;
    private readonly string? _swaggerCsp;
    private readonly string? _spaCsp;

    public CspMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _swaggerCsp = ReadPolicy(configuration, CspConstants.SwaggerSettingsName);
        _spaCsp = ReadPolicy(configuration, CspConstants.SpaSettingsName);
    }

    [UsedImplicitly]
    public Task InvokeAsync(HttpContext context, NonceProvider nonceProvider)
    {
        context.Response.Headers[CspConstants.HeaderName] = GetCspValue(context.Request.Path, nonceProvider.Nonce);
        return _next(context);
    }

    private string GetCspValue(PathString path, string nonce)
    {
        if (path.StartsWithSegments("/swagger", StringComparison.OrdinalIgnoreCase))
        {
            return WithNonce(_swaggerCsp, nonce);
        }

        if (path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase))
        {
            return StrictCsp;
        }

        return WithNonce(_spaCsp, nonce);
    }

    private static string WithNonce(string? policy, string nonce) =>
        policy?.Replace(CspConstants.NoncePlaceholder, nonce, StringComparison.Ordinal) ?? StrictCsp;

    // An empty value (e.g. an unset Web Deploy parameter) counts as missing and falls back to the strict policy;
    // a policy without the placeholder can never carry the nonce, so that misconfiguration fails at startup.
    private static string? ReadPolicy(IConfiguration configuration, string settingsName)
    {
        var value = configuration.GetValue<string>(settingsName);
        if (String.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (!value.Contains(CspConstants.NoncePlaceholder, StringComparison.Ordinal))
        {
            throw new InvalidOperationException(
                $"Configuration value '{settingsName}' must contain the nonce placeholder '{CspConstants.NoncePlaceholder}'.");
        }

        return value;
    }
}
