using System.Security.Principal;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.TestImpersonation
{
    public class TestPrincipal : IPrincipal
    {
        private const string TestUsername = "test_user@xyz.com";

        private TestPrincipal(string userName)
        {
            UserName = userName;
            Identity = new GenericIdentity(userName);
        }

        public string UserName { get; }

        public bool IsInRole(string role)
        {
            return true;
        }


        public IIdentity Identity { get; }

        public static TestPrincipal CreateDefault()
        {
            return new TestPrincipal(TestUsername);
        }
    }
}