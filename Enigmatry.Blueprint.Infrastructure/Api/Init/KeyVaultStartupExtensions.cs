using Azure.Identity;
using Enigmatry.Blueprint.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Infrastructure.Api.Init
{
    public static class KeyVaultStartupExtensions
    {
        public static void AppAddAzureKeyVault(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            var settings = configuration.ReadKeyVaultSettings();

            if (settings.Enabled)
            {
                builder.Configuration.AddAzureKeyVault(new Uri($@"https://{settings.Name}.vault.azure.net"), new DefaultAzureCredential());
            }
        }
    }
}
