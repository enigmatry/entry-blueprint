using Enigmatry.Blueprint.Model;
using Enigmatry.Blueprint.Model.Identity;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.TestImpersonation
{
    internal class CurrentUserProvider : ICurrentUserProvider
    {
        public CurrentUserProvider(TestPrincipal principal, ITimeProvider timeProvider)
        {
            User = User.Create(principal.UserName, "some name", timeProvider.Now);
        }

        public User User { get; }

        public bool IsAuthenticated => true;
    }
}