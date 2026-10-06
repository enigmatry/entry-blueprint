using System.Security.Cryptography;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;

/// <summary>
/// Holds the CSP nonce for the current request. Registered as a scoped service so that every request
/// gets its own value, which the CSP header and the served HTML must share.
/// </summary>
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
