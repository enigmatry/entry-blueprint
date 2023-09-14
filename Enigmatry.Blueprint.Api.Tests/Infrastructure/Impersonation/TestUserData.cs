using System.Security.Claims;
using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Blueprint.Domain.Users;
using Enigmatry.Blueprint.Domain.Users.Commands;
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.Impersonation;

public static class TestUserData
{
    public static readonly Guid TestUserId = new("8af459b7-d6b3-4b0a-8200-66257811e66e");
    private static readonly Guid TestUserRoleId = Role.SystemAdminRoleId;
    private const string TestEmailAddress = "test_user@xyz.com";

    public static ClaimsPrincipal CreateClaimsPrincipal() =>
        new(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, value: TestEmailAddress)
        }, "TestAuth"));

    public static User CreateTestUser() =>
        User.Create(new CreateOrUpdateUser.Command
        {
            RoleId = TestUserRoleId,
            EmailAddress = TestEmailAddress,
            FullName = "INTEGRATION_TEST"
        }).WithId(TestUserId);
}
