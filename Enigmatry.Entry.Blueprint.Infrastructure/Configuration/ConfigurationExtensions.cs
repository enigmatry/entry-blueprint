using Enigmatry.Entry.Blueprint.Core.Settings;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Configuration;

public static class ConfigurationExtensions
{
    public static bool AppUseDeveloperExceptionPage(this IConfiguration configuration) => configuration.GetValue("UseDeveloperExceptionPage", false);

    public static AppSettings ReadAppSettings(this IConfiguration configuration) => configuration.ReadSettingsSection<AppSettings>("App");

    public static KeyVaultSettings ReadKeyVaultSettings(this IConfiguration configuration) =>
        configuration.ReadSettingsSection<KeyVaultSettings>(KeyVaultSettings.SectionName);

    public static T ReadSettingsSection<T>(this IConfiguration configuration, string sectionName)
    {
        var sectionSettings = configuration.GetSection(sectionName).Get<T>();
        return sectionSettings == null
            ? throw new InvalidOperationException($"Section is missing from configuration. Section Name: {sectionName}")
            : sectionSettings;
    }
}
