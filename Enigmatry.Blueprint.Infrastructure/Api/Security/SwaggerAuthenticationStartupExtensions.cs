using Enigmatry.Entry.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace Enigmatry.Blueprint.Infrastructure.Api.Security;

public static class SwaggerAuthenticationStartupExtensions
{
    public static void AppUseSwaggerWithAzureAdAuth(this IApplicationBuilder app, IConfiguration configuration)
    {
        var aadOptions = configuration.GetSection(AuthenticationStartupExtensions.AzureAdSection).Get<MicrosoftIdentityOptions>()!;
        app.AppUseSwaggerWithOAuth2Client(aadOptions.ClientId!);
    }

    public static void AppAddSwaggerWithAzureAdAuth(this IServiceCollection services, IConfiguration configuration, string appTitle)
    {
        var aadOptions = configuration.GetSection(AuthenticationStartupExtensions.AzureAdSection).Get<MicrosoftIdentityOptions>()!;
        var authorityUrl = $"{aadOptions.Instance}/{aadOptions.TenantId ?? aadOptions.Domain}";

        if (aadOptions.SignUpSignInPolicyId != null)
        {
            authorityUrl += $"/{aadOptions.SignUpSignInPolicyId}";
        }

        var scopesDictionary = aadOptions.Scope.ToDictionary(scope => scope, _ => "");

        services.AppAddSwaggerWithAuthorizationCode(
            appTitle,
            $"{authorityUrl}/oauth2/v2.0/authorize",
            $"{authorityUrl}/oauth2/v2.0/token",
            scopesDictionary
        );
    }
}
