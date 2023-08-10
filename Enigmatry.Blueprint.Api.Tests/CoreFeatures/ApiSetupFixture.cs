using System.Net.Http.Json;
using Enigmatry.Blueprint.Api.Features.Users;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Api;
using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Blueprint.Domain.Identity.Commands;
using Enigmatry.Entry.AspNetCore.Tests.SystemTextJson.Http;
using Enigmatry.Entry.Core.Paging;
using Enigmatry.Blueprint.Model.Tests.Identity;
using FluentAssertions;
using Enigmatry.Blueprint.Domain.Authorization;

namespace Enigmatry.Blueprint.Api.Tests.CoreFeatures;

// Fixture that validates if Api project has been setup correctly
// Uses arbitrary controller (in this case UserController) and verifies if basic operations, e.g. get, post, validations are correctly setup.  
[Category("integration")]
public class ApiSetupFixture : IntegrationFixtureBase
{
    private User _user = null!;

    [SetUp]
    public void SetUp()
    {
        _user = new UserBuilder()
            .WithUserName("john_doe@john.doe")
            .WithName("John Doe")
            .WithRoleId(Role.SystemAdminRoleId);

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
    public async Task GivenValidUserId_GetById_ReturnsUserDetails()
    {
        var user = await Client.GetAsync<GetUserDetails.Response>($"users/{_user.Id}");

        await Verify(user);
    }

    [Test]
    public async Task GivenNonExistingUserId_GetById_ReturnsNotFound()
    {
        var response = await Client.GetAsync($"users/{Guid.NewGuid()}");

        response.Should().BeNotFound();
    }

    [Test]
    public async Task TestCreate()
    {
        var command = new CreateOrUpdateUser.Command { Name = "some user", UserName = "someuser@test.com", RoleId = Role.SystemAdminRoleId };
        var user =
            await Client.PostAsync<CreateOrUpdateUser.Command, GetUserDetails.Response>("users", command);

        await Verify(user);
    }

    [Test]
    public async Task TestUpdate()
    {
        var command = new CreateOrUpdateUser.Command { Id = _user.Id, Name = "some user", UserName = "someuser@test.com", RoleId = Role.SystemAdminRoleId };
        var user =
            await Client.PostAsync<CreateOrUpdateUser.Command, GetUserDetails.Response>("users", command);

        await Verify(user);
    }

    [TestCase("some user", "invalid email", "userName", "is not a valid email address.")]
    [TestCase("", "someuser@test.com", "name", "must not be empty.")]
    [TestCase("some user", "", "userName", "must not be empty.")]
    [TestCase("John Doe", "john_doe@john.doe", "userName", "Username already taken")]
    public async Task TestCreateReturnsValidationErrors(string name,
        string userName,
        string validationField,
        string validationErrorMessage)
    {
        var command = new CreateOrUpdateUser.Command { Name = name, UserName = userName };
        var response = await Client.PostAsJsonAsync("users", command);

        response.Should().BeBadRequest().And.ContainValidationError(validationField, validationErrorMessage);
    }
}
