using Enigmatry.Entry.Blueprint.Infrastructure.Configuration;
using Enigmatry.Entry.AspNetCore.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Reflection;
using Enigmatry.Entry.Core.Helpers;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;

public static class SerilogStartupExtension
{
    public static LoggerConfiguration AppConfigureSerilog(this LoggerConfiguration loggerConfiguration, IConfiguration configuration)
    {
        var loggerSectionExists = configuration.GetSection("Serilog").Exists();
        var logLevel = configuration.GetSection("Serilog:MinimumLevel:Default").Value ?? String.Empty;
        if (!loggerSectionExists || logLevel == "__defaultLogLevel__")
        {
            return loggerConfiguration;
        }
        loggerConfiguration
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithProcessId()
            .Enrich.WithMachineName()
            .Enrich.With(new OperationIdEnricher())
            .Enrich.WithProperty("AppVersion", Assembly.GetEntryAssembly()!.GetName().Version!);

        // for enabling self diagnostics see https://github.com/serilog/serilog/wiki/Debugging-and-Diagnostics
        // Serilog.Debugging.SelfLog.Enable(Console.Error);

        return loggerConfiguration;
    }

    public static void AddAppInsightsToSerilog(this LoggerConfiguration loggerConfiguration, IConfiguration configuration, IServiceProvider serviceProvider)
    {
        var settings = configuration.ReadApplicationInsightsSettings();
        if (settings == null || !settings.ConnectionString.HasContent())
        {
            return;
        }

        var telemetryConfiguration = serviceProvider.GetRequiredService<TelemetryConfiguration>();
        loggerConfiguration.WriteTo.ApplicationInsights(telemetryConfiguration, TelemetryConverter.Traces);
    }
}
