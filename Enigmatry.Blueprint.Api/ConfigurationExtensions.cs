using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Api
{
    public static class ConfigurationExtensions
    {
        public static bool UseDeveloperExceptionPage(this IConfiguration configuration) =>
            configuration.GetValue("UseDeveloperExceptionPage", false);
    }
}
