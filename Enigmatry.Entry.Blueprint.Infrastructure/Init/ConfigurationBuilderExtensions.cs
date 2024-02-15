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
            var keyVaultUri = new Uri($"https://{settings.Name}.vault.azure.net");
            builder.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());
        }
    }
}
