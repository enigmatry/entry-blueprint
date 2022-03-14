using System.Security.Claims;
using System.Security.Principal;
using Autofac;
using Enigmatry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Blueprint.Infrastructure.Autofac.Modules;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Infrastructure.Api.Startup;

public static class ContainerBuilderStartupExtensions
{
#pragma warning disable CA1801 // Review unused parameters
#pragma warning disable IDE0060 // Remove unused parameter
    public static void AppRegisterModules(this ContainerBuilder builder, IConfiguration configuration)
#pragma warning restore IDE0060 // Remove unused parameter
#pragma warning restore CA1801 // Review unused parameters
    {
        builder.RegisterModule<ConfigurationModule>();
        builder.Register(GetPrincipal)
            .As<IPrincipal>().InstancePerLifetimeScope();
        builder.RegisterModule(new ServiceModule
        {
            Assemblies = new[]
            {
                AssemblyFinder.Find("Enigmatry.BuildingBlocks.Infrastructure"),
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
        var user = httpContextAccessor.HttpContext.User;
        return user;
    }
}
