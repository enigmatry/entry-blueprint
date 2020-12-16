using System.Linq;
using System.Net.Mime;
using Enigmatry.Blueprint.Infrastructure.Configuration;
using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace Enigmatry.Blueprint.Infrastructure.Api.Init
{
    public static class HealthChecksStartupExtensions
    {
        public static void AppMapHealthCheck(this IEndpointRouteBuilder endpoints) =>
            endpoints.MapHealthChecks("/healthcheck", new HealthCheckOptions()
            {
                // Specify a custom ResponseWriter, so we can return json with additional information,
                // Otherwise it will just return plain text with the status.
                ResponseWriter = async (context, report) =>
                {
                    var result = JsonConvert.SerializeObject(
                        (status: report.Status.ToString(), entries: report.Entries.Select(e => (key: e.Key, value: e.Value.Status.ToString()))));
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            }).RequireAuthorization();

        public static void AppAddHealthChecks(this IServiceCollection services, IConfiguration configuration) =>

            // Here we can configure the different health checks:
            services.AddHealthChecks()

                // Check the sql server connection
                //.AddSqlServer(configuration["ConnectionStrings:BlueprintContext"], "SELECT 1")
                // Check the EF Core Context
                .AddDbContextCheck<BlueprintContext>()

                // Check a Custom url
                /*.AddUrlGroup(new Uri("https://api.myapplication.com/v1/something.json"), "API ping Test",
                    HealthStatus.Degraded)*/
                // Check metrics
                .AddPrivateMemoryHealthCheck(1024 * 1024 * 200, "Available memory test", HealthStatus.Degraded)

                // Check Redis
                //.AddRedis(redisConnectionString: configuration["ConnectionStrings:RedisConnection"],
                //    name: "Redis",
                //    failureStatus: HealthStatus.Degraded)
                // We can also push the results to Application Insights. This will be done every 30 seconds
                // Can be checked from the Azure Portal under metrics, by selecting the azure.applicationinsights namespace.
                .AddApplicationInsightsPublisher(configuration.ReadApplicationInsightsSettings().InstrumentationKey);
    }
}
