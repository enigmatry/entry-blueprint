using System.Security.Principal;
using Autofac;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.TestImpersonation;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.Autofac
{
    [UsedImplicitly]
    public class TestModule : Module
    {
        protected override void Load(ContainerBuilder builder) => SetupCurrentUser(builder);

        private static void SetupCurrentUser(ContainerBuilder builder)
        {
            TestPrincipal principal = TestPrincipal.CreateDefault();

            builder.Register(c => principal).As<IPrincipal>().InstancePerLifetimeScope();

            builder.Register(c => new TestCurrentUserIdProvider(principal.UserId))
                .As<ICurrentUserIdProvider>()
                .InstancePerLifetimeScope();

            builder.Register(c => new TestCurrentUserProvider(principal))
                .As<ICurrentUserProvider>().InstancePerLifetimeScope();
        }
    }
}
