using System.Security.Cryptography;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;

// Registered as a scoped service: one nonce per request, shared by the CSP header and the served HTML.
public sealed class NonceProvider : IDisposable
{
    private readonly RandomNumberGenerator _random = RandomNumberGenerator.Create();

    public NonceProvider()
    {
        var bytes = new byte[32];
        _random.GetBytes(bytes);
        Nonce = Convert.ToBase64String(bytes);
    }

    public string Nonce { get; }

    public void Dispose() => _random.Dispose();
}
