using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Blueprint.Domain.Identity.Commands;
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.TestImpersonation;

internal class TestCurrentUserProvider : ICurrentUserProvider
{
    public TestCurrentUserProvider(TestPrincipal principal)
    {
        User = User.Create(new CreateOrUpdateUser.Command
        {
            UserName = principal.UserName,
            Name = "INTEGRATION_TEST"
        }).WithId(principal.UserId);
    }

    public Guid? UserId => User?.Id;
    public User? User { get; }

    public bool IsAuthenticated => true;
}
