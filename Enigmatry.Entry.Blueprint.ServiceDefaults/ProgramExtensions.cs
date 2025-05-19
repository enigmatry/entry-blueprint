using Azure.Monitor.OpenTelemetry.AspNetCore;
using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Enigmatry.Entry.ServiceDefaults;

// Adds common .NET Aspire services: service discovery, resilience, health checks, and OpenTelemetry.
// This project should be referenced by each service project in your solution.
// To learn more about using this project, see https://aka.ms/dotnet/aspire/service-defaults
public static class ProgrgamExtensions
{
    public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
    {
        builder.ConfigureOpenTelemetry();

        builder.AddDefaultHealthChecks();

        builder.Services.AddServiceDiscovery();

        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            // Turn on resilience by default
            http.AddStandardResilienceHandler();

            // Turn on service discovery by default
            http.AddServiceDiscovery();
        });

        return builder;
    }

    public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
    {
        builder.Logging.ConfigureOpenTelemetryLogging();

        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation();
            })
            .WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation()
                    // Uncomment the following line to enable gRPC instrumentation (requires the OpenTelemetry.Instrumentation.GrpcNetClient package)
                    //.AddGrpcClientInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation(options =>
                    {
                        options.SetDbStatementForText = true;
                    });
            })
            .AddOpenTelemetryExporters(builder.Configuration);

        return builder;
    }

    public static IServiceCollection AddOpenTelemetryWorkerService(this IServiceCollection services, IConfiguration configuration, string serviceName)
    {
        services.AddOpenTelemetry()
            .WithTracing(tracing =>
            {
                if (IsAzureMonitorConfigured(configuration))
                {
                    // Add a resource with service information
                    tracing.SetResourceBuilder(ResourceBuilder.CreateDefault()
                        .AddService(serviceName))
                        // Register your ActivitySource - use the same name as in your listener
                        .AddSource(serviceName)
                        .AddAzureMonitorTraceExporter();
                }
            })
            .WithMetrics(metrics =>
            {
                if (IsAzureMonitorConfigured(configuration))
                {
                    metrics.AddAzureMonitorMetricExporter();
                }
            })
            .AddOpenTelemetryExporters(configuration);

        return services;
    }

    public static void ConfigureOpenTelemetryLogging(this ILoggingBuilder loggingBuilder)
    {
        // Clear all logging providers and add OpenTelemetry logging provider
        loggingBuilder.ClearProviders();
        loggingBuilder.AddOpenTelemetry(options =>
        {
            options.IncludeFormattedMessage = true;
            options.IncludeScopes = true;
        });
    }

    private static void AddOpenTelemetryExporters(this OpenTelemetryBuilder builder, IConfiguration configuration)
    {
        if (IsOtlpCollectorConfigured(configuration))
        {
            builder.Services.AddOpenTelemetry().UseOtlpExporter();
        }

        // Uncomment the following lines to enable the Azure Monitor exporter (requires the Azure.Monitor.OpenTelemetry.AspNetCore package)
        if (IsAzureMonitorConfigured(configuration))
        {
            builder.Services.AddOpenTelemetry()
                .UseAzureMonitor();
        }
    }
    public static IHostApplicationBuilder AddDefaultHealthChecks(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            // Add a default liveness check to ensure app is responsive
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        return builder;
    }

    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        // Adding health checks endpoints to applications in non-development environments has security implications.
        // See https://aka.ms/dotnet/aspire/healthchecks for details before enabling these endpoints in non-development environments.
        if (app.Environment.IsDevelopment())
        {
            // All health checks must pass for app to be considered ready to accept traffic after starting
            app.MapHealthChecks("/health");

            // Only health checks tagged with the "live" tag must pass for app to be considered alive
            app.MapHealthChecks("/alive", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("live")
            });
        }

        return app;
    }

    private static bool IsAzureMonitorConfigured(IConfiguration configuration)
        => !String.IsNullOrEmpty(configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

    private static bool IsOtlpCollectorConfigured(IConfiguration configuration)
        => !String.IsNullOrWhiteSpace(configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

}
