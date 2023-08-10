using Autofac;
using Enigmatry.Blueprint.Infrastructure.Identity;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules;

public class IdentityModule : Module
{
    protected override void Load(ContainerBuilder builder) => builder.RegisterType<CurrentUserProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();
}
