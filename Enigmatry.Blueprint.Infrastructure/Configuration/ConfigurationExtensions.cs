using System;
using Enigmatry.Blueprint.Core.Settings;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Infrastructure.Configuration
{
    public static class ConfigurationExtensions
    {
        public static bool AppUseDeveloperExceptionPage(this IConfiguration configuration) => configuration.GetValue("UseDeveloperExceptionPage", false);

        public static AppSettings ReadAppSettings(this IConfiguration configuration)
        {
            return configuration.ReadSettingsSection<AppSettings>("App");
        }

        public static ApplicationInsightsSettings ReadApplicationInsightsSettings(this IConfiguration configuration)
        {
            return configuration.ReadSettingsSection<ApplicationInsightsSettings>("ApplicationInsights");
        }

        public static T ReadSettingsSection<T>(this IConfiguration configuration, string sectionName)
        {
            var sectionSettings = configuration.GetSection(sectionName).Get<T>();
            if (sectionSettings == null)
            {
                throw new InvalidOperationException($"Section is missing from configuration. Section Name: {sectionName}");
            }
            return sectionSettings;
        }
    }
}
