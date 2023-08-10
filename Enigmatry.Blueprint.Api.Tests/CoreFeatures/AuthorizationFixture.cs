using Enigmatry.Blueprint.Api.Tests.Infrastructure.Api;
using System.Net;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Impersonation;
using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Blueprint.Domain.Identity.Commands;
using FluentAssertions;
using Enigmatry.Entry.AspNetCore.Tests.SystemTextJson.Http;
using System.Net.Http.Json;

namespace Enigmatry.Blueprint.Api.Tests.CoreFeatures;

[Category("integration")]
public class AuthorizationFixture : IntegrationFixtureBase
{
    [SetUp]
    public void SetUp()
    {
        var testRole = new Role { Name = "TestRole" };
        testRole.SetPermissions(QueryDb<Permission>().Where(p => p.Id == PermissionId.UsersRead));

        var currentUser = QueryCurrentUser();
        currentUser.Update(new CreateOrUpdateUser.Command
        {
            RoleId = testRole.Id
        });
        AddAndSaveChanges(testRole);
    }

    [Test]
    public async Task UserWithPermissionIsAllowed()
    {
        var response = await Client.GetAsync("users");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    public async Task UserWithoutPermissionIsNotAllowed()
    {
        var command = new CreateOrUpdateUser.Command
        {
            Name = "some user",
            UserName = "someuser@test.com",
            RoleId = Role.SystemAdminRoleId
        };
        var response = await Client.PostAsJsonAsync("users", command, HttpSerializationOptions.Options);
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Test]
    public async Task EndpointWithoutAuthorizeAttributeIsAllowed()
    {
        var response = await Client.GetAsync("profile");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    private User QueryCurrentUser() => QueryDb<User>().First(u => u.Id == TestUserData.TestUserId);
}
