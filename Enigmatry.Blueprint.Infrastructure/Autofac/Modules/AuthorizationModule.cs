using Autofac;
using Enigmatry.Blueprint.ApplicationServices.Authorization;
using Enigmatry.Entry.AspNetCore.Authorization;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules;

public class AuthorizationModule : Module
{
    protected override void Load(ContainerBuilder builder) => builder.RegisterType<DefaultAuthorizationProvider>().As<IAuthorizationProvider>().InstancePerLifetimeScope();
}
