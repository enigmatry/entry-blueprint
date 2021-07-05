using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Blueprint.Infrastructure.Api.Init
{
    public static class SettingsStartupExtensions
    {
#pragma warning disable CA1801 // Review unused parameters
#pragma warning disable IDE0060 // Remove unused parameter
        public static void AppAddSettings(this IServiceCollection services, IConfiguration configuration)
#pragma warning restore IDE0060 // Remove unused parameter
#pragma warning restore CA1801 // Review unused parameters
        {
            // Options for particular external services
            // services.Configure<CustomSettings>(configuration.GetSection("App:CustomSettings"));
        }
    }
}
