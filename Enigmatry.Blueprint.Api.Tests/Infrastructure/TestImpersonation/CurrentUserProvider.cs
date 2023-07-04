using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Blueprint.Domain.Identity.Commands;
using Enigmatry.Entry.Core.Entities;
using User = Enigmatry.Blueprint.Domain.Identity.User;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.TestImpersonation;

internal class TestCurrentUserProvider : ICurrentUserProvider
{
    public TestCurrentUserProvider(TestPrincipal principal)
    {
        User = PrepareUserData(principal.UserName, principal.UserId);
    }

    public Guid? UserId => User?.Id;
    public User? User { get; }

    public bool IsAuthenticated => true;

    private User PrepareUserData(string userName, Guid userId)
    {
        var user = User.Create(new CreateOrUpdateUser.Command
        {
            UserName = userName,
            Name = "INTEGRATION_TEST",
            RoleId = Role.SystemAdminRoleId
        }).WithId(userId);

        var permissions = new List<Permission>
        {
            Permission.Create("PERMISSION_1"),
            Permission.Create("PERMISSION_2"),
            Permission.Create("PERMISSION_3"),
            Permission.Create("PERMISSION_4")
        };
        var role = Role.Create("INTEGRATION_TEST_ROLE", permissions).WithId(Guid.NewGuid());

        typeof(User).GetProperty(nameof(Role))?.SetValue(user, role);

        return user;
    }
}
