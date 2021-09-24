using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Infrastructure.Api.Authorization;
using Enigmatry.Blueprint.Infrastructure.Configuration;
using Enigmatry.Blueprint.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Enigmatry.Blueprint.Infrastructure.Api.Init
{
    public static class HealthChecksStartupExtensions
    {
        public static void AppMapHealthCheck(this IEndpointRouteBuilder endpoints) =>
            endpoints.MapHealthChecks("/healthcheck", new HealthCheckOptions()
            {
                // Specify a custom ResponseWriter, so we can return json with additional information,
                // Otherwise it will just return plain text with the status.
                ResponseWriter = WriteResponse,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            }).RequireAuthorization(HealthCheckTokenRequirement.Name);

        public static void AppAddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            // Here we can configure the different health checks:
            var healthChecks = services.AddHealthChecks()

                // Check the sql server connection
                //.AddSqlServer(configuration["ConnectionStrings:YessaContext"], "SELECT 1")

                // Check the EF Core Context
                .AddDbContextCheck<BlueprintContext>()

                // Check a Custom url
                /*.AddUrlGroup(new Uri("https://api.myapplication.com/v1/something.json"), "API ping Test",
                    HealthStatus.Degraded)*/

                // Check Redis
                //.AddRedis(redisConnectionString: configuration["ConnectionStrings:RedisConnection"],
                //    name: "Redis",
                //    failureStatus: HealthStatus.Degraded)

                // Check metrics
                .AddPrivateMemoryHealthCheck(1024 * 1024 * configuration.ReadHealthCheckSettings().MaximumAllowedMemoryInMegaBytes, "Available memory test", HealthStatus.Degraded);

            // We can also push the results to Application Insights. This will be done every 30 seconds
            // Can be checked from the Azure Portal under metrics, by selecting the azure.applicationinsights namespace.
            var appInsightsKey = configuration.ReadApplicationInsightsSettings().InstrumentationKey;
            if (!String.IsNullOrEmpty(appInsightsKey))
            {
                healthChecks.AddApplicationInsightsPublisher(configuration.ReadApplicationInsightsSettings().InstrumentationKey, true);
            }

            services.AddAuthorization(options =>
            {
                options.AddPolicy(HealthCheckTokenRequirement.Name, policy => policy.Requirements.Add(new HealthCheckTokenRequirement(configuration.ReadHealthCheckSettings().RequiredToken)));
            });
            services.AddSingleton<IAuthorizationHandler, HealthCheckTokenHandler>();
        }

        private static Task WriteResponse(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using var stream = new MemoryStream();
            using (var writer = new Utf8JsonWriter(stream, options))
            {
                writer.WriteStartObject();
                writer.WriteString("status", result.Status.ToString());
                writer.WriteStartObject("results");
                foreach (var entry in result.Entries)
                {
                    writer.WriteStartObject(entry.Key);
                    writer.WriteString("status", entry.Value.Status.ToString());
                    writer.WriteEndObject();
                }
                writer.WriteEndObject();
                writer.WriteEndObject();
            }

            var json = Encoding.UTF8.GetString(stream.ToArray());

            return context.Response.WriteAsync(json);
        }
    }
}
