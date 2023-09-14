using Autofac;
using Enigmatry.Blueprint.Infrastructure.Identity;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules;

[UsedImplicitly]
public class IdentityModule : Module
{
    protected override void Load(ContainerBuilder builder) => builder.RegisterType<CurrentUserProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();
}
