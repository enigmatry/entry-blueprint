using Enigmatry.Blueprint.Api.Features;
using Enigmatry.Blueprint.Api.Features.Users;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Api;
using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Blueprint.Domain.Identity.Commands;
using Enigmatry.Blueprint.Model.Tests.Identity;
using Enigmatry.Entry.AspNetCore.Tests.SystemTextJson.Http;
using Enigmatry.Entry.Core.Paging;

namespace Enigmatry.Blueprint.Api.Tests;

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
            .WithUserName("john_doe@john.doe")
            .WithName("John Doe");

        AddAndSaveChanges(_user);
    }

    [Test]
    public async Task TestGetAll()
    {
        var users = (await Client.GetAsync<PagedResponse<GetUsers.Response.Item>>(
                new Uri("users", UriKind.RelativeOrAbsolute), new KeyValuePair<string, string>("SortBy", "UserName")))
            ?.Items.ToList()!;
        await Verify(users);
    }

    [Test]
    public async Task TestLookup()
    {
        var uri = $"users/lookup?pageSize=100";
        var users = await Client.GetAsync<PagedResponse<LookupResponse>>(uri);
        await Verify(users!.Items);
    }

    [Test]
    public async Task TestGetById()
    {
        var user = await Client.GetAsync<GetUserDetails.Response>($"users/{_user.Id}");
        await Verify(user);
    }

    [Test]
    public async Task TestCreate()
    {
        var command = new CreateOrUpdateUser.Command { Name = "some user", UserName = "someuser@test.com" };
        var user =
            await Client.PostAsync<CreateOrUpdateUser.Command, GetUserDetails.Response>("users", command);

        await Verify(user);
    }

    [Test]
    public async Task TestUpdate()
    {
        var command = new CreateOrUpdateUser.Command { Id = _user.Id, Name = "some user", UserName = "someuser@test.com" };
        var user =
            await Client.PostAsync<CreateOrUpdateUser.Command, GetUserDetails.Response>("users", command);

        await Verify(user);
    }
}
