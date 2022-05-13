using Azure.Identity;
using Enigmatry.BuildingBlocks.Core.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Infrastructure.Api.Init
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AppAddAzureKeyVault(this WebApplicationBuilder builder)
        {
            var enabled = Environment.GetEnvironmentVariable("ASPNETCORE_HOSTINGSTARTUP__KEYVAULT__CONFIGURATIONENABLED") ?? String.Empty;
            var keyVaultName = Environment.GetEnvironmentVariable("ASPNETCORE_HOSTINGSTARTUP__KEYVAULT__CONFIGURATIONVAULT") ?? String.Empty;

            if (enabled.Equals("true", StringComparison.OrdinalIgnoreCase) && keyVaultName.HasContent())
            {
                builder.Configuration.AddAzureKeyVault(new Uri(keyVaultName), new DefaultAzureCredential());
            }
        }
    }
}
