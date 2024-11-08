using Autofac;
using Enigmatry.Entry.AspNetCore.Authorization;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Infrastructure.Authorization;
using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;

[UsedImplicitly]
public class AuthorizationModule : Module
{
    protected override void Load(ContainerBuilder builder) =>
        builder.RegisterType<DefaultAuthorizationProvider>().As<IAuthorizationProvider<PermissionId>>().InstancePerLifetimeScope();
}
