using Microsoft.AspNetCore.Authorization;

namespace Enigmatry.Blueprint.Infrastructure.Api.Authorization
{
    public class HealthCheckTokenRequirement : IAuthorizationRequirement
    {
        public const string Name = "HealthCheckToken";

        public HealthCheckTokenRequirement(string token)
        {
            Token = token;
        }

        public string Token { get; private set; }
    }
}
