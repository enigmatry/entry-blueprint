using Enigmatry.Entry.Blueprint.Infrastructure.Configuration;
using Enigmatry.Entry.AspNetCore.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.ApplicationInsights.TelemetryConverters;
using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;

public static class SerilogStartupExtension
{
    public static void AppConfigureSerilog(this WebApplicationBuilder builder)
    {
        LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithProcessId()
            .Enrich.WithMachineName()
            .Enrich.With(new OperationIdEnricher())
            .Enrich.WithProperty("AppVersion", Assembly.GetEntryAssembly()!.GetName().Version!);

        AddAppInsightsToSerilog(loggerConfiguration, builder.Configuration);

        Log.Logger = loggerConfiguration.CreateLogger();

        // for enabling self diagnostics see https://github.com/serilog/serilog/wiki/Debugging-and-Diagnostics
        // Serilog.Debugging.SelfLog.Enable(Console.Error);
    }

    private static void AddAppInsightsToSerilog(LoggerConfiguration loggerConfiguration, IConfiguration configuration)
    {
        var settings = configuration.ReadApplicationInsightsSettings();
        loggerConfiguration.WriteTo.ApplicationInsights(new TraceTelemetryConverter(), settings.SerilogLogsRestrictedToMinimumLevel);
    }
}
