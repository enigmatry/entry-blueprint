﻿using Enigmatry.Blueprint.Api.Tests.Infrastructure.Api;
using System.Net;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Impersonation;
using Enigmatry.Blueprint.Domain.Authorization;
using FluentAssertions;
using Enigmatry.Entry.AspNetCore.Tests.SystemTextJson.Http;
using System.Net.Http.Json;
using Enigmatry.Blueprint.Domain.Users;
using Enigmatry.Blueprint.Domain.Users.Commands;

namespace Enigmatry.Blueprint.Api.Tests.CoreFeatures;

[Category("integration")]
public class AuthorizationFixture : IntegrationFixtureBase
{
    private Role? _testRole;

    [SetUp]
    public void SetUp()
    {
        _testRole = new Role { Name = "TestRole" };
        _testRole.SetPermissions(QueryDb<Permission>().Where(p => p.Id == PermissionId.UsersRead));

        var currentUser = QueryCurrentUser();
        currentUser.Update(new CreateOrUpdateUser.Command
        {
            RoleId = _testRole.Id
        });
        AddAndSaveChanges(_testRole);
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
            FullName = "some user",
            EmailAddress = "someuser@test.com",
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

    [TearDown]
    public async Task TearDown()
    {
        var currentUser = QueryCurrentUser();
        currentUser.Update(new CreateOrUpdateUser.Command
        {
            RoleId = Role.SystemAdminRoleId
        });

        if (_testRole != null)
        {
            await DeleteByIdsAndSaveChanges<Role, Guid>(_testRole.Id);
        }
    }

    private User QueryCurrentUser() => QueryDb<User>().First(u => u.Id == TestUserData.TestUserId);
}
