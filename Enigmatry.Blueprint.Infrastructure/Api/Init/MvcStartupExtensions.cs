using Enigmatry.Entry.AspNetCore.Filters;
using Enigmatry.Blueprint.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Blueprint.Infrastructure.Api.Init;

public static class MvcStartupExtensions
{
    // The following adds support for controllers, API-related features, and views, not pages. 
    // Views are required for templating with RazorTemplatingEngine(e.g. emails)
    //.AddControllersWithViews(options => options.ConfigureMvc(configuration))
    public static IMvcBuilder AppAddMvc(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddControllers(options => options.ConfigureMvc(configuration))
            .AppAddFluentValidation();

    private static void ConfigureMvc(this MvcOptions options, IConfiguration configuration)
    {
        options.Filters.Add(new CancelSavingTransactionAttribute());
        options.Filters.Add(new HandleExceptionsFilter(configuration.AppUseDeveloperExceptionPage()));
    }
}
