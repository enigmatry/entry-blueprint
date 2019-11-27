using System.Security.Claims;
using System.Security.Principal;
using Autofac;
using Enigmatry.Blueprint.ApplicationServices.Identity;
using Enigmatry.Blueprint.Infrastructure;
using Enigmatry.Blueprint.Infrastructure.Autofac.Modules;
using Enigmatry.Blueprint.Infrastructure.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Api
{
    public static class ContainerBuilderStartupExtensions
    {
        public static void AppRegisterModules(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterModule<ConfigurationModule>();
            builder.Register(GetPrincipal)
                .As<IPrincipal>().InstancePerLifetimeScope();
            builder.RegisterModule(new ServiceModule
                {Assemblies = new[] {typeof(UserService).Assembly, typeof(TimeProvider).Assembly}});
            builder.RegisterModule<EntityFrameworkModule>();
            builder.RegisterModule<IdentityModule>();
            builder.RegisterModule<EmailModule>();
            builder.RegisterModule(new EventBusModule
            {
                AzureServiceBusEnabled = configuration.ReadAppSettings().ServiceBus.AzureServiceBusEnabled
            });
            builder.RegisterModule<TemplatingModule>();
        }

        private static ClaimsPrincipal GetPrincipal(IComponentContext c)
        {
            var httpContextAccessor = c.Resolve<IHttpContextAccessor>();
            ClaimsPrincipal user = httpContextAccessor.HttpContext.User;
            return user;
        }
    }
}
