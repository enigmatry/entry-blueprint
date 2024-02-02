using Azure.Identity;
using Enigmatry.Entry.Blueprint.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Init;

public static class ConfigurationBuilderExtensions
{
    public static void AppAddAzureKeyVault(this IConfigurationBuilder builder, IConfiguration configuration)
    {
        var settings = configuration.ReadKeyVaultSettings();

        if (settings is { Enabled: true })
        {
            builder.AppAddAzureKeyVault($@"https://{settings.Name}.vault.azure.net");
        }
    }

    private static IConfigurationBuilder AppAddAzureKeyVault(this IConfigurationBuilder builder, string? keyVaultUri)
    {
        if (keyVaultUri != null)
        {
            builder.AddAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());
        }
        return builder;
    }
}
