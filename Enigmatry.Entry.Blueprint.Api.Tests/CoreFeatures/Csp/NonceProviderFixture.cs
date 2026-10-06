using Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;
using Shouldly;

namespace Enigmatry.Entry.Blueprint.Api.Tests.CoreFeatures.Csp;

[Category("unit")]
public class NonceProviderFixture
{
    [Test]
    public void Nonce_IsBase64Of32Bytes()
    {
        using var provider = new NonceProvider();

        var bytes = Convert.FromBase64String(provider.Nonce);

        bytes.Length.ShouldBe(32);
    }

    [Test]
    public void Nonce_IsStableWithinOneProvider()
    {
        using var provider = new NonceProvider();

        provider.Nonce.ShouldBe(provider.Nonce);
    }

    [Test]
    public void TwoProviders_ProduceDifferentNonces()
    {
        using var first = new NonceProvider();
        using var second = new NonceProvider();

        first.Nonce.ShouldNotBe(second.Nonce);
    }
}
