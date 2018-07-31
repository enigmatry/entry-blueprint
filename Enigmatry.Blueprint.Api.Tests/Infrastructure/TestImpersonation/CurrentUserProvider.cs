using System;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Model;
using Enigmatry.Blueprint.Model.Identity;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.TestImpersonation
{
    internal class CurrentUserProvider : ICurrentUserProvider
    {
        // static so that we remember Id bettwen each resolve of CurrentUserProvider
        private static Guid userId = Guid.NewGuid();

        public CurrentUserProvider(TestPrincipal principal, ITimeProvider timeProvider)
        {
            User = User.Create(new UserCreateOrUpdateCommand
            {
                UserName = principal.UserName,
                Name = "INTEGRATION_TEST"
            }).WithId(userId);
        }

        public Guid UserId => User.Id;
        public User User { get; }

        public bool IsAuthenticated => true;
    }
}