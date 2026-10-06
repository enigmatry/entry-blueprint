namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;

public static class CspConstants
{
    public static readonly string HeaderName = "Content-Security-Policy";

    /// <summary>Configuration key of the nonce-based policy used for Swagger UI responses.</summary>
    public static readonly string SettingsName = "ContentSecurityPolicyValue";

    /// <summary>Configuration key of the nonce-based policy used for the Angular application.</summary>
    public static readonly string SpaSettingsName = "SpaContentSecurityPolicyValue";

    /// <summary>
    /// Token that appears in the configured policies and in index.html; replaced with the per-request nonce.
    /// </summary>
    public static readonly string NoncePlaceholder = "**PLACEHOLDER_NONCE_SERVER**";
}
