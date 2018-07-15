using Enigmatry.Blueprint.Model;
using Enigmatry.Blueprint.Model.Identity;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.TestImpersonation
{
    internal class CurrentUserProvider : ICurrentUserProvider
    {
        public CurrentUserProvider(TestPrincipal principal, ITimeProvider timeProvider)
        {
            User = User.Create(new UserCreateDto
            {
                UserName = principal.UserName,
                Name = "John Doe",
                CreatedOn = timeProvider.Now
            });
        }

        public User User { get; }

        public bool IsAuthenticated => true;
    }
}