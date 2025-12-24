using System.Reflection;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Enigmatry.Entry.Blueprint.ServiceDefaults;
public static class OpenTelemetryExtensions
{
    public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
    {
        builder.Logging.ConfigureOpenTelemetryLogging(builder.Configuration);

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
                    .AddHttpClientInstrumentation();
                // Uncomment the following line to enable sampling on collecting traces. 
                //.SetSampler(new TraceIdRatioBasedSampler(0.5));

                if (builder.Environment.IsDevelopment())
                {
                    tracing.AddSqlClientInstrumentation();
                }
            })
            .AddOpenTelemetryExporters(builder.Configuration);

        return builder;
    }

    public static IServiceCollection AddOpenTelemetryWorkerService(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceName = Assembly.GetExecutingAssembly().FullName!;
        services.AddOpenTelemetry()
            .WithTracing(tracing =>
            {
                if (IsAzureMonitorConfigured(configuration))
                {
                    tracing
                        .SetResourceBuilder(ResourceBuilder.CreateDefault()
                        .AddService(serviceName))
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
            });

        return services;
    }

    public static void ConfigureOpenTelemetryLogging(this ILoggingBuilder loggingBuilder, IConfiguration configuration)
    {
        // Clear all logging providers and add OpenTelemetry logging provider
        loggingBuilder.ClearProviders();
        loggingBuilder.AddOpenTelemetry(options =>
        {
            options.IncludeFormattedMessage = true;
            options.IncludeScopes = true;
            if (IsAzureMonitorConfigured(configuration))
            {
                options.AddAzureMonitorLogExporter();
            }
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

    private static bool IsAzureMonitorConfigured(IConfiguration configuration)
        => !String.IsNullOrEmpty(configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

    private static bool IsOtlpCollectorConfigured(IConfiguration configuration)
        => !String.IsNullOrWhiteSpace(configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);
}
