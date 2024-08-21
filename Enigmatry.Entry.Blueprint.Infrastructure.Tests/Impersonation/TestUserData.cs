using System.Security.Claims;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Blueprint.Domain.Users.Commands;
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Tests.Impersonation;

public static class TestUserData
{
    public static readonly Guid TestUserId = new("8af459b7-d6b3-4b0a-8200-66257811e66e");
    private const string TestEmailAddress = "test_user@xyz.com";
    private static readonly DateTimeOffset CreatedDate = new(2024, 1, 9, 0, 0, 0, TimeSpan.Zero);

    public static ClaimsPrincipal CreateClaimsPrincipal() =>
        new(new ClaimsIdentity([new Claim(ClaimTypes.Upn, TestEmailAddress)], "TestAuth"));

    public static User CreateTestUser() => CreateUser(TestUserId, TestEmailAddress, "INTEGRATION_TEST", Role.SystemAdminRoleId);

    public static User CreateSystemUser() => CreateUser(User.SystemUserId, "N/A", "System User", Role.SystemAdminRoleId);

    private static User CreateUser(Guid id, string email, string fullName, Guid roleId)
    {
        var user = User.Create(new CreateOrUpdateUser.Command
        {
            Id = null,
            RoleId = roleId,
            EmailAddress = email,
            FullName = fullName,
            UserStatusId = UserStatusId.Active
        }).WithId(id);
        return user;
    }
}
