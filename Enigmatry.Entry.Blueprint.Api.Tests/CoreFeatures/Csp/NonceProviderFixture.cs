using Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;
using Shouldly;

namespace Enigmatry.Entry.Blueprint.Api.Tests.CoreFeatures.Csp;

[Category("unit")]
public class NonceProviderFixture
{
    [Test]
    public void Nonce_IsBase64Of32Bytes()
    {
        var bytes = Convert.FromBase64String(new NonceProvider().Nonce);

        bytes.Length.ShouldBe(32);
    }

    [Test]
    public void TwoProviders_ProduceDifferentNonces()
    {
        new NonceProvider().Nonce.ShouldNotBe(new NonceProvider().Nonce);
    }
}
