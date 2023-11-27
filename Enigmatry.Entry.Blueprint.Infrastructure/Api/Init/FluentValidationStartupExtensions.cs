using System.Reflection;
using Enigmatry.Entry.AspNetCore.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;

public static class FluentValidationStartupExtensions
{
    public static void AppConfigureFluentValidation(this IApplicationBuilder _) =>
        ValidatorOptions.Global.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;

    public static IServiceCollection AppAddFluentValidation(this IServiceCollection services) =>
        services.AddValidatorsFromAssemblies(new List<Assembly>()
        {
            AssemblyFinder.DomainAssembly, AssemblyFinder.ApplicationServicesAssembly, AssemblyFinder.ApiAssembly
        });
}
