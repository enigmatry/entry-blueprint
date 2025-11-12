using System.Net;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Api;
using Shouldly;

namespace Enigmatry.Entry.Blueprint.Api.Tests.CoreFeatures;

[Category("integration")]
public class UnauthenticatedAccessFixture : IntegrationFixtureBase
{
    public UnauthenticatedAccessFixture() => DisableUserAuthentication();

    [Test]
    public async Task EndpointWithAuthorizeAttributeIsNotAllowed()
    {
        var response = await Client.GetAsync("api/users");
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task EndpointWithNoAuthorizeAttributeIsNotAllowed()
    {
        var response = await Client.GetAsync("api/profile");
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}
