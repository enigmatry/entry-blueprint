using Enigmatry.Blueprint.Api.Filters;
using Enigmatry.Blueprint.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Blueprint.Api.Init
{
    public static class MvcStartupExtensions
    {
        public static IMvcBuilder AppAddMvc(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                // The following adds support for controllers, API-related features, and views, not pages. 
                // Views are required for templating with RazorTemplatingEngine(e.g. emails)
                .AddControllersWithViews(options => options.ConfigureMvc(configuration))
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson()
                .AppAddLocalization()
                .AppAddFluentValidation();
        }

        private static void ConfigureMvc(this MvcOptions options, IConfiguration configuration)
        {
            options.Filters.Add(new CancelSavingTransactionAttribute());
            options.Filters.Add(new HandleExceptionsFilter(configuration.AppUseDeveloperExceptionPage()));
        }
    }
}
