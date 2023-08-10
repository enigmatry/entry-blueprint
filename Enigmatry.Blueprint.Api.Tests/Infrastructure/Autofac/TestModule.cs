using System.Security.Principal;
using Autofac;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Impersonation;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.Autofac;

[UsedImplicitly]
public class TestModule : Module
{
    protected override void Load(ContainerBuilder builder) => SetupCurrentUser(builder);

    private static void SetupCurrentUser(ContainerBuilder builder)
        => builder.Register(_ => TestUserData.CreateClaimsPrincipal()).As<IPrincipal>().InstancePerLifetimeScope();
}
