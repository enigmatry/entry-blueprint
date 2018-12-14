using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules
{
    public static class ConfigurationExtensions
    {
        public static bool SensitiveDataLoggingEnabled(this IConfiguration configuration) =>
            configuration.GetValue("SensitiveDataLoggingEnabled", false);
    }
}