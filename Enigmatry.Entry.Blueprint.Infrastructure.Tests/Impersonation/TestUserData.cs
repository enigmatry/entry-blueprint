using System.Security.Claims;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Blueprint.Domain.Users.Commands;
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Impersonation;

public static class TestUserData
{
    public static readonly Guid TestUserId = new("8af459b7-d6b3-4b0a-8200-66257811e66e");
    private static readonly Guid TestUserRoleId = Role.SystemAdminRoleId;
    private const string TestEmailAddress = "test_user@xyz.com";
    private const string SchedulerEmailAddress = "scheduler";
    private static readonly DateTimeOffset CreatedDate = new(2024, 1, 9, 0, 0, 0, TimeSpan.Zero);

    public static ClaimsPrincipal CreateClaimsPrincipal() =>
        new(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Upn, value: TestEmailAddress)
        }, "TestAuth"));

    public static User CreateTestUser()
    {
        var user = User.Create(new CreateOrUpdateUser.Command
        {
            RoleId = TestUserRoleId, EmailAddress = TestEmailAddress, FullName = "INTEGRATION_TEST"
        }).WithId(TestUserId);
        user.SetCreated(CreatedDate, TestUserId);
        user.SetUpdated(CreatedDate, TestUserId);
        return user;
    }

    public static User CreateSchedulerUser()
    {
        var user = User.Create(new CreateOrUpdateUser.Command
        {
            RoleId = TestUserRoleId, EmailAddress = SchedulerEmailAddress, FullName = "Scheduler user"
        }).WithId(User.SchedulerUserId);
        user.SetCreated(CreatedDate, User.SchedulerUserId);
        user.SetUpdated(CreatedDate, User.SchedulerUserId);
        return user;
    }
}
