using System.Security.Claims;
using System.Security.Principal;
using Autofac;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Startup;

public static class ContainerBuilderStartupExtensions
{
#pragma warning disable IDE0060 // Remove unused parameter
    public static void AppRegisterModules(this ContainerBuilder builder, IConfiguration configuration)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        builder.Register(GetPrincipal)
            .As<IPrincipal>().InstancePerLifetimeScope();

        builder.RegisterAssemblyModules(AssemblyFinder.InfrastructureAssembly);

        builder.RegisterModule(new ServiceModule
        {
            Assemblies = new[]
            {
                AssemblyFinder.Find("Enigmatry.Entry.Infrastructure"),
                AssemblyFinder.ApplicationServicesAssembly,
                AssemblyFinder.InfrastructureAssembly
            }
        });
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
