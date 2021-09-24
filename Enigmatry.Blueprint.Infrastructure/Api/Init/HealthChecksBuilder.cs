using Enigmatry.Blueprint.Infrastructure.Data;
using Enigmatry.BuildingBlocks.Core.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Blueprint.Infrastructure.Api.Init
{
    public static class HealthChecksBuilder
    {
#pragma warning disable CA1801 // Review unused parameters
#pragma warning disable IDE0060 // Remove unused parameter
        public static IHealthChecksBuilder BuildHealthChecks(IServiceCollection services, HealthCheckSettings healthCheckSettings) =>
#pragma warning restore IDE0060 // Remove unused parameter
#pragma warning restore CA1801 // Review unused parameters
            services.AddHealthChecks()
                // Check the sql server connection (Does not work with managed identities!)
                // .AddSqlServer(_configuration["ConnectionStrings:BlueprintContext"], "SELECT 1")

                // Check the EF Core Context
                .AddDbContextCheck<BlueprintContext>()

                // Check a Custom url
                //.AddUrlGroup(new Uri("https://api.myapplication.com/v1/something.json"), "External API ping Test", HealthStatus.Degraded);

                // Check Redis
                //.AddRedis(redisConnectionString: configuration["ConnectionStrings:RedisConnection"], name: "Redis", failureStatus: HealthStatus.Degraded)

                // Check metrics (this example is already included in the Enigmatry Healthcheck Building Block):
                //.AddPrivateMemoryHealthCheck(1024 * 1024 * healthCheckSettings.MaximumAllowedMemoryInMegaBytes, "Available memory test", HealthStatus.Degraded);
                ;
    }
}
