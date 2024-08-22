﻿using System.Security.Claims;
using System.Security.Principal;
using Autofac;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;
using Microsoft.AspNetCore.Http;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Startup;

public static class ContainerBuilderStartupExtensions
{
    public static void AppRegisterModules(this ContainerBuilder builder)
    {
        builder.RegisterAssemblyModules(AssemblyFinder.InfrastructureAssembly);

        builder.RegisterModule(new ServiceModule
        {
            Assemblies =
            [
                AssemblyFinder.Find("Enigmatry.Entry.Infrastructure"), AssemblyFinder.ApplicationServicesAssembly,
                AssemblyFinder.InfrastructureAssembly
            ]
        });
    }

    public static void AppRegisterClaimsPrincipalProvider(this ContainerBuilder builder) =>
        builder.Register(GetPrincipalFromHttpContext)
            .As<IPrincipal>().InstancePerLifetimeScope();

    private static ClaimsPrincipal GetPrincipalFromHttpContext(IComponentContext c)
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
