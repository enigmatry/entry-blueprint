using Autofac;
using Enigmatry.Entry.Blueprint.Infrastructure.Identity;
using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;

[UsedImplicitly]
public class IdentityModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ClaimsProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterType<CurrentUserProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();
    }
}
