using Enigmatry.Blueprint.Domain.Identity;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.TestImpersonation;

internal class TestCurrentUserIdProvider : ICurrentUserIdProvider
{
    public Guid? UserId { get; }
    public bool IsAuthenticated => true;

    public TestCurrentUserIdProvider(Guid userId)
    {
        UserId = userId;
    }
}
