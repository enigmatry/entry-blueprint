using System;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Model.Identity;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.TestImpersonation
{
    internal class TestCurrentUserProvider : ICurrentUserProvider
    {
        public TestCurrentUserProvider(TestPrincipal principal)
        {
            User = User.Create(new UserCreateOrUpdateCommand
            {
                UserName = principal.UserName,
                Name = "INTEGRATION_TEST"
            }).WithId(principal.UserId);
        }

        public Guid? UserId => User?.Id;
        public User? User { get; }

        public bool IsAuthenticated => true;
    }
}
