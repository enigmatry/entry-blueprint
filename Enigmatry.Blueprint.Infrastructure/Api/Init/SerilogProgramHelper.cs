using Enigmatry.Blueprint.Infrastructure.Configuration;
using Enigmatry.BuildingBlocks.AspNetCore.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.ApplicationInsights.TelemetryConverters;
using System.Reflection;

namespace Enigmatry.Blueprint.Infrastructure.Api.Init;

public static class SerilogProgramHelper
{
    public static void AppConfigureSerilog(IConfiguration configuration)
    {
        LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithProcessId()
            .Enrich.WithMachineName()
            .Enrich.With(new OperationIdEnricher())
            .Enrich.WithProperty("AppVersion", Assembly.GetEntryAssembly()!.GetName().Version!);

        AddAppInsightsToSerilog(loggerConfiguration, configuration);

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
