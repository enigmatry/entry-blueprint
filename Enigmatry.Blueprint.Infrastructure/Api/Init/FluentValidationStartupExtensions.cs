using System.Reflection;
using Enigmatry.Entry.AspNetCore.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Blueprint.Infrastructure.Api.Init;

public static class FluentValidationStartupExtensions
{
#pragma warning disable IDE0060 // Remove unused parameter
    public static void AppConfigureFluentValidation(this IApplicationBuilder app) => ValidatorOptions.Global.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;
#pragma warning restore IDE0060 // Remove unused parameter

    public static void AppAddFluentValidationApiBehaviorOptions(this ApiBehaviorOptions options) =>
        options.InvalidModelStateResponseFactory = context =>
            context.HttpContext.CreateValidationProblemDetailsResponse(context.ModelState);

    public static IServiceCollection AppAddFluentValidation(this IServiceCollection services) =>
        services.AddValidatorsFromAssemblies(new List<Assembly>() { AssemblyFinder.DomainAssembly, AssemblyFinder.ApplicationServicesAssembly, AssemblyFinder.ApiAssembly });
}
