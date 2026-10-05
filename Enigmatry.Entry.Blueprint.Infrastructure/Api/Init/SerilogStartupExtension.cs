using System.Reflection;
using Enigmatry.Entry.AspNetCore.OpenTelemetry;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Settings.Configuration;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;

public static class SerilogStartupExtension
{
    private const string SecurityLogConfigurationSection = "SecuritySerilog";

    public static LoggerConfiguration AppConfigureSerilog(this LoggerConfiguration loggerConfiguration, IConfiguration configuration)
    {
        var loggerSectionExists = configuration.GetSection("Serilog").Exists();
        if (!loggerSectionExists)
        {
            // we might not have logger section in the tests only
            return loggerConfiguration;
        }
        loggerConfiguration
            .ReadFrom.Configuration(configuration)
            .ApplyCommonEnrichers();

        // for enabling self diagnostics see https://github.com/serilog/serilog/wiki/Debugging-and-Diagnostics
        // Serilog.Debugging.SelfLog.Enable(Console.Error);

        return loggerConfiguration;
    }

    /// <summary>
    /// Configures the dedicated security/audit logger. Sinks and level overrides come entirely from the
    /// <c>SecuritySerilog</c> configuration section (e.g. a dedicated audit file per environment); where
    /// that section is absent the logger has no sinks and security events are not persisted.
    /// </summary>
    public static LoggerConfiguration AppConfigureSecuritySerilog(this LoggerConfiguration loggerConfiguration, IConfiguration configuration) =>
        loggerConfiguration
            .ApplyCommonEnrichers()
            .ReadFrom.Configuration(configuration, new ConfigurationReaderOptions { SectionName = SecurityLogConfigurationSection });

    private static LoggerConfiguration ApplyCommonEnrichers(this LoggerConfiguration loggerConfiguration) =>
        loggerConfiguration
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithProcessId()
            .Enrich.WithMachineName()
            .Enrich.With(new OperationIdEnricher())
            .Enrich.WithProperty("AppVersion", Assembly.GetEntryAssembly()!.GetName().Version!);
}
