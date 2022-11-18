using Enigmatry.Entry.AspNetCore.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Blueprint.Infrastructure.Api.Init;

public static class FluentValidationStartupExtensions
{
#pragma warning disable IDE0060 // Remove unused parameter
    public static void AppConfigureFluentValidation(this IApplicationBuilder app) =>
        ValidatorOptions.Global.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;
#pragma warning restore IDE0060 // Remove unused parameter

    public static void AppAddFluentValidationApiBehaviorOptions(this ApiBehaviorOptions options) =>
        options.InvalidModelStateResponseFactory = context =>
            context.HttpContext.CreateValidationProblemDetailsResponse(context.ModelState);

    public static IMvcBuilder AppAddFluentValidation(this IMvcBuilder mvcBuilder) =>
        mvcBuilder.AddFluentValidation(fv =>
        {
            // disables standard data annotations validation
            // https://fluentvalidation.net/aspnet.html#asp-net-core
            fv.DisableDataAnnotationsValidation = false;
            fv.ImplicitlyValidateChildProperties = true;
            fv.RegisterValidatorsFromAssemblies(new[]
            {
                AssemblyFinder.DomainAssembly, AssemblyFinder.ApiAssembly
            });
        });
}
