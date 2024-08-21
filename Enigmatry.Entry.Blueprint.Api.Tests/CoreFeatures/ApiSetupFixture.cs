using System.Net.Http.Json;
using AutoMapper;
using Enigmatry.Entry.AspNetCore.Tests.SystemTextJson.Http;
using Enigmatry.Entry.Blueprint.Api.Features.Users;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Api;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Domain.Tests.Users;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Blueprint.Domain.Users.Commands;
using Enigmatry.Entry.Core.Paging;
using FluentAssertions;

namespace Enigmatry.Entry.Blueprint.Api.Tests.CoreFeatures;

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
        var command = new CreateOrUpdateUser.Command
        {
            Id = null,
            FullName = "some user",
            EmailAddress = "someuser@test.com",
            RoleId = Role.SystemAdminRoleId,
            UserStatusId = UserStatusId.Inactive
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

    [TestCase("some user", "invalid email", "emailAddress", "is not a valid email address.")]
    [TestCase("", "someuser@test.com", "fullName", "must not be empty.")]
    [TestCase("some user", "", "emailAddress", "must not be empty.")]
    [TestCase("John Doe", "john_doe@john.doe", "emailAddress", "EmailAddress is already taken")]
    public async Task TestCreateReturnsValidationErrors(string name,
        string emailAddress,
        string validationField,
        string validationErrorMessage)
    {
        var command = new CreateOrUpdateUser.Command
        {
            Id = null,
            FullName = name,
            EmailAddress = emailAddress,
            RoleId = Role.SystemAdminRoleId,
            UserStatusId = UserStatusId.Inactive
        };
        var response = await Client.PostAsJsonAsync("users", command, HttpSerializationOptions.Options);

        response.Should().BeBadRequest().And.ContainValidationError(validationField, validationErrorMessage);
    }

    [Test]
    public void TestAutoMapperMappings()
    {
        var mapper = Resolve<IMapper>();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
