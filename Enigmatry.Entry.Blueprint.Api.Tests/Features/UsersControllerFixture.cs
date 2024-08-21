using Enigmatry.Entry.AspNetCore.Tests.SystemTextJson.Http;
using Enigmatry.Entry.Blueprint.Api.Features;
using Enigmatry.Entry.Blueprint.Api.Features.Users;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Api;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Domain.Tests.Users;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Blueprint.Domain.Users.Commands;
using Enigmatry.Entry.Core.Paging;

namespace Enigmatry.Entry.Blueprint.Api.Tests.Features;

[Category("integration")]

// Basic example of an integration test.
// For every public api method add appropriate test.
// Integration tests should be used for happy flows.
// For un-happy flows (e.g. edge cases), or complex business rules
// write unit tests.
public class UsersControllerFixture : IntegrationFixtureBase
{
    private User _user = null!;

    [SetUp]
    public void SetUp()
    {
        _user = new UserBuilder()
            .WithEmailAddress("john_doe@john.doe")
            .WithFullName("John Doe")
            .WithRoleId(Role.SystemAdminRoleId)
            .WithStatusId(UserStatusId.Active);

        AddAndSaveChanges(_user);
    }

    [Test]
    public async Task TestGetAll()
    {
        var users = (await Client.GetAsync<PagedResponse<GetUsers.Response.Item>>(
                new Uri("users", UriKind.RelativeOrAbsolute), new KeyValuePair<string, string>("SortBy", "EmailAddress")))
            ?.Items.ToList()!;

        await Verify(users);
    }

    [Test]
    public async Task TestGetById()
    {
        var user = await Client.GetAsync<GetUserDetails.Response>($"users/{_user.Id}");

        await Verify(user);
    }

    [Test]
    public async Task TestGetRoles()
    {
        var user = await Client.GetAsync<IEnumerable<LookupResponse<Guid>>>($"users/roles");

        await Verify(user);
    }

    [Test]
    public async Task TestGetStatuses()
    {
        var user = await Client.GetAsync<IEnumerable<LookupResponse<UserStatusId>>>($"users/statuses");

        await Verify(user);
    }

    [Test]
    public async Task TestCreate()
    {
        var command = new CreateOrUpdateUser.Command
        {
            Id = null,
            FullName = "some user",
            EmailAddress = "someuser@test.com",
            RoleId = Role.SystemAdminRoleId,
            UserStatusId = UserStatusId.Active
        };
        var user =
            await Client.PostAsync<CreateOrUpdateUser.Command, GetUserDetails.Response>("users", command);

        await Verify(user);
    }

    [Test]
    public async Task TestUpdate()
    {
        var command = new CreateOrUpdateUser.Command
        {
            Id = _user.Id,
            FullName = "some user",
            EmailAddress = "someuser@test.com",
            RoleId = Role.SystemAdminRoleId,
            UserStatusId = UserStatusId.Inactive
        };
        var user =
            await Client.PostAsync<CreateOrUpdateUser.Command, GetUserDetails.Response>("users", command);

        await Verify(user);
    }
}
