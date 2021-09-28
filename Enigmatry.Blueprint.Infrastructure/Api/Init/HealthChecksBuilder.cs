using System.Diagnostics.CodeAnalysis;
using Enigmatry.Blueprint.Infrastructure.Data;
using Enigmatry.BuildingBlocks.Core.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Blueprint.Infrastructure.Api.Init
{
    public static class HealthChecksBuilder
    {
        [SuppressMessage("Usage", "CA1801:Review unused parameters", Justification = "HealthCheckSettings can be used to configure project-specific health checks.")]
        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "HealthCheckSettings can be used to configure project-specific health checks.")]
        public static IHealthChecksBuilder BuildHealthChecks(IServiceCollection services, HealthCheckSettings healthCheckSettings) =>
            services.AddHealthChecks()
                // Check the sql server connection (Does not work with managed identities!)
                // .AddSqlServer(_configuration["ConnectionStrings:BlueprintContext"], "SELECT 1")

                // Check the EF Core Context
                .AddDbContextCheck<BlueprintContext>()

                // Check a Custom url
                //.AddUrlGroup(new Uri("https://api.myapplication.com/v1/something.json"), "External API ping Test", HealthStatus.Degraded);

                // Check Redis
                //.AddRedis(redisConnectionString: configuration["ConnectionStrings:RedisConnection"], name: "Redis", failureStatus: HealthStatus.Degraded)
                ;
    }
}
