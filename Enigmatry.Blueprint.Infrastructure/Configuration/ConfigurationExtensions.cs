using System;
using Enigmatry.Blueprint.Core.Settings;
using Enigmatry.BuildingBlocks.Core.Settings;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Infrastructure.Configuration
{
    public static class ConfigurationExtensions
    {
        public static bool AppUseDeveloperExceptionPage(this IConfiguration configuration) => configuration.GetValue("UseDeveloperExceptionPage", false);

        public static AppSettings ReadAppSettings(this IConfiguration configuration) => configuration.ReadSettingsSection<AppSettings>("App");

        public static ApplicationInsightsSettings ReadApplicationInsightsSettings(this IConfiguration configuration) =>
            configuration.ReadSettingsSection<ApplicationInsightsSettings>(ApplicationInsightsSettings.ApplicationInsightsSectionName);

        public static HealthCheckSettings ReadHealthCheckSettings(this IConfiguration configuration) =>
            configuration.ReadSettingsSection<HealthCheckSettings>(HealthCheckSettings.HealthChecksSectionName);

        public static T ReadSettingsSection<T>(this IConfiguration configuration, string sectionName)
        {
            var sectionSettings = configuration.GetSection(sectionName).Get<T>();
            return sectionSettings == null
                ? throw new InvalidOperationException($"Section is missing from configuration. Section Name: {sectionName}")
                : sectionSettings;
        }
    }
}
