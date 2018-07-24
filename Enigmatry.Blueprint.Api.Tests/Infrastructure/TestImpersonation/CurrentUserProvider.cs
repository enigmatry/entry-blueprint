using System;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Model;
using Enigmatry.Blueprint.Model.Identity;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.TestImpersonation
{
    internal class CurrentUserProvider : ICurrentUserProvider
    {
        public CurrentUserProvider(TestPrincipal principal, ITimeProvider timeProvider)
        {
            User = User.Create(new UserCreateUpdateDto
            {
                UserName = principal.UserName,
                Name = "John Doe"
            }).CreatedOn(timeProvider.Now, Guid.Empty);
        }

        public Guid UserId => User.Id;
        public User User { get; }

        public bool IsAuthenticated => true;
    }
}