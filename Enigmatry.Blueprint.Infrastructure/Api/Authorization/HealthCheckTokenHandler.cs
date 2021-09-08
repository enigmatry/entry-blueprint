using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Enigmatry.Blueprint.Infrastructure.Api.Authorization
{
    public class HealthCheckTokenHandler : AuthorizationHandler<HealthCheckTokenRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HealthCheckTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HealthCheckTokenRequirement requirement)
        {
            if (String.IsNullOrEmpty(requirement.Token) ||
                (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Request.Query["token"].ToString() == requirement.Token))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
