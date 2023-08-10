using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace Enigmatry.Blueprint.Infrastructure.Api.Security
{
    public static class AuthenticationStartupExtensions
    {
        private const string ConfigurationSection = "App:AzureAdB2C";
        private const string EmailsClaim = "emails";

        public static void AppAddAuthentication(this IServiceCollection services, IConfiguration configuration) =>
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(options =>
                    {
                        configuration.Bind(ConfigurationSection, options);
                        options.TokenValidationParameters.NameClaimType = EmailsClaim; // Mapping AzureAd 'emails' claim to Principal.Identity.Name
                    },
                    options => { configuration.Bind(ConfigurationSection, options); });
    }
}
