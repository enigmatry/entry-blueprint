using Enigmatry.Entry.AspNetCore.Tests.SystemTextJson.Http;
using Enigmatry.Entry.Blueprint.Api.Features.Authorization;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Api;

namespace Enigmatry.Entry.Blueprint.Api.Tests.Features;

[Category("integration")]
public class ProfileControllerFixture : IntegrationFixtureBase
{
    [Test]
    public async Task TestGetProfileDetails()
    {
        var profile = await Client.GetAsync<GetUserProfile.Response>("api/profile");
        await Verify(profile);
    }
}
