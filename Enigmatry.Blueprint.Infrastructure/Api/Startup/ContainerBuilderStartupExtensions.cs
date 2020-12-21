using System;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using Autofac;
using Enigmatry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Blueprint.Infrastructure.Autofac.Modules;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Infrastructure.Api.Startup
{
    public static class ContainerBuilderStartupExtensions
    {
#pragma warning disable CA1801 // Review unused parameters
        public static void AppRegisterModules(this ContainerBuilder builder, IConfiguration configuration)
#pragma warning restore CA1801 // Review unused parameters
        {
            builder.RegisterModule<ConfigurationModule>();
            builder.Register(GetPrincipal)
                .As<IPrincipal>().InstancePerLifetimeScope();
            builder.RegisterModule(new ServiceModule
            {
                Assemblies = new[]
            {
                AssemblyFinder.Find("Enigmatry.Blueprint.BuildingBlocks.Infrastructure"),
                AssemblyFinder.ApplicationServicesAssembly,
                AssemblyFinder.InfrastructureAssembly
            }
            });
            builder.RegisterModule<EntityFrameworkModule>();
        }

        private static ClaimsPrincipal GetPrincipal(IComponentContext c)
        {
            var httpContextAccessor = c.Resolve<IHttpContextAccessor>();
            if (httpContextAccessor.HttpContext == null)
            {
                throw new InvalidOperationException("HttpContext is null");
            }
            ClaimsPrincipal user = httpContextAccessor.HttpContext.User;
            return user;
        }
    }
}
