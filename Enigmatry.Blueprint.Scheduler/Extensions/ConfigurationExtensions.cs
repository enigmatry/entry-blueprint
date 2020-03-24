using System;
using System.IO;
using Enigmatry.Blueprint.Scheduler.Configurations;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Scheduler.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfiguration Configuration { get; } =
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                    true)
                .Build();

        public static AppSettings ReadAppSettings(this IConfiguration configuration)
        {
            return configuration.ReadSettingsSection<AppSettings>("App");
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
