using Autofac;
using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Blueprint.Infrastructure.Authorization;
using Enigmatry.Entry.AspNetCore.Authorization;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules;

[UsedImplicitly]
public class AuthorizationModule : Module
{
    protected override void Load(ContainerBuilder builder) =>
        builder.RegisterType<DefaultAuthorizationProvider>().As<IAuthorizationProvider<PermissionId>>().InstancePerLifetimeScope();
}
