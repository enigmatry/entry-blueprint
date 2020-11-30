using System;
using System.Security.Principal;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.TestImpersonation
{
    public class TestPrincipal : IPrincipal
    {
        private const string TestUsername = "test_user@xyz.com";

        private TestPrincipal(Guid userId, string userName)
        {
            UserId = userId;
            UserName = userName;
            Identity = new GenericIdentity(userName);
        }

        public Guid UserId { get; }
        public string UserName { get; }

        public bool IsInRole(string role) => true;


        public IIdentity Identity { get; }

        public static TestPrincipal CreateDefault() => new TestPrincipal(Guid.NewGuid(), TestUsername);
    }
}
