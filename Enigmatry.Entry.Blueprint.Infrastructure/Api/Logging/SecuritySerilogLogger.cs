using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Logging;

/// <summary>
/// Standalone Serilog logger for security and audit events. It is deliberately independent of the
/// Microsoft.Extensions.Logging / OpenTelemetry pipeline so that <c>ClearProviders()</c> in the Aspire
/// ServiceDefaults cannot remove it and so that security events never get mixed into the application log.
/// </summary>
public sealed class SecuritySerilogLogger : IDisposable
{
    public ILogger Logger { get; }

    public SecuritySerilogLogger(IConfiguration configuration) =>
        Logger = new LoggerConfiguration().AppConfigureSecuritySerilog(configuration).CreateLogger();

    public void Dispose() => (Logger as IDisposable)?.Dispose();
}
