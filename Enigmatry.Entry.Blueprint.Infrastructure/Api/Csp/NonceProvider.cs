using System.Security.Cryptography;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;

// Registered as a scoped service: one nonce per request, shared by the CSP header and the served HTML.
public sealed class NonceProvider
{
    public string Nonce { get; } = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
}
