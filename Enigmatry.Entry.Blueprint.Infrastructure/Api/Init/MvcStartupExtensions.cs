using System.Text.Json.Serialization;
using Enigmatry.Entry.AspNetCore.Filters;
using Enigmatry.Entry.SmartEnums.SystemTextJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;

public static class MvcStartupExtensions
{
    // The following adds support for controllers, API-related features, and views, not pages. 
    // Views are required for templating with RazorTemplatingEngine(e.g. emails)
    //.AddControllersWithViews(options => options.ConfigureMvc(configuration))
    public static IMvcBuilder AppAddMvc(this IServiceCollection services) =>
        services
            .AddControllers(options => options.ConfigureMvc()).AddJsonOptions(options =>
            {
                var converters = options.JsonSerializerOptions.Converters;
                converters.AppRegisterJsonConverters();
            });

    private static void ConfigureMvc(this MvcOptions options) => options.Filters.Add(new CancelSavingTransactionAttribute());

    public static void AppRegisterJsonConverters(this IList<JsonConverter> converters) =>
        converters.EntryAddSmartEnumJsonConverters(SmartEnumConverterType.ValueConverter,
            [AssemblyFinder.ApiAssembly, AssemblyFinder.DomainAssembly]);
}
