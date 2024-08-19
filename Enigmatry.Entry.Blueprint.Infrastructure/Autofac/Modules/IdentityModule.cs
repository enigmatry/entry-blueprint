using Autofac;
using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Blueprint.Infrastructure.Identity;
using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;

[UsedImplicitly]
public class IdentityModule<T> : Module where T : ICurrentUserProvider
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ClaimsProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterType<T>().AsImplementedInterfaces().InstancePerLifetimeScope();
    }
}
