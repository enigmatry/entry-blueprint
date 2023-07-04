using Enigmatry.Blueprint.Api.Features.Authorization;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Api;
using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Blueprint.Domain.Tests.Authorization;
using Enigmatry.Entry.AspNetCore.Tests.SystemTextJson.Http;

namespace Enigmatry.Blueprint.Api.Tests.Features.Authorization;

[Category("integration")]
public class ProfileControllerFixture : IntegrationFixtureBase
{
    private Role _role = null!;

    [SetUp]
    public void SetUp()
    {
        _role = new RoleBuilder()
            .WithName("name1")
            .WithPermissions(new List<Permission> { PermissionConstants.First, PermissionConstants.Second });

        AddAndSaveChanges(_role);
    }

    [Test]
    public async Task TestGetProfileDetails()
    {
        var role = await Client.GetAsync<GetUserProfile.Response>("profile");

        await Verify(role);
    }
}
