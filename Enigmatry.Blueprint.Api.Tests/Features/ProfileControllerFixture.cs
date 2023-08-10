using Enigmatry.Blueprint.Api.Features.Authorization;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Api;
using Enigmatry.Entry.AspNetCore.Tests.SystemTextJson.Http;

namespace Enigmatry.Blueprint.Api.Tests.Features;

[Category("integration")]
public class ProfileControllerFixture : IntegrationFixtureBase
{
    [Test]
    public async Task TestGetProfileDetails()
    {
        var profile = await Client.GetAsync<GetUserProfile.Response>("profile");
        await Verify(profile);
    }
}
