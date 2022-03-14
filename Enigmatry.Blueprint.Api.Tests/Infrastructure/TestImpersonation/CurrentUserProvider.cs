using Enigmatry.BuildingBlocks.Core.Entities;
using Enigmatry.Blueprint.Model.Identity;
using Enigmatry.Blueprint.Model.Identity.Commands;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.TestImpersonation;

internal class TestCurrentUserProvider : ICurrentUserProvider
{
    public TestCurrentUserProvider(TestPrincipal principal)
    {
        User = User.Create(new UserCreateOrUpdate.Command
        {
            UserName = principal.UserName,
            Name = "INTEGRATION_TEST"
        }).WithId(principal.UserId);
    }

    public Guid? UserId => User?.Id;
    public User? User { get; }

    public bool IsAuthenticated => true;
}
