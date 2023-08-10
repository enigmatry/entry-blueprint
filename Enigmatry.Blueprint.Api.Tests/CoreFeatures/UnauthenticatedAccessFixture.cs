using System.Net;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Api;
using FluentAssertions;

namespace Enigmatry.Blueprint.Api.Tests.CoreFeatures
{
    public class UnauthenticatedAccessFixture : IntegrationFixtureBase
    {
        public UnauthenticatedAccessFixture() => DisableUserAuthentication();

        [Test]
        public async Task EndpointWithAuthorizeAttributeIsNotAllowed()
        {
            var response = await Client.GetAsync("users");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task EndpointWithNoAuthorizeAttributeIsNotAllowed()
        {
            var response = await Client.GetAsync("profile");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
