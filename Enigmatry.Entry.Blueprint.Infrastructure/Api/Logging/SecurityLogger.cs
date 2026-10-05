using Enigmatry.Entry.Blueprint.Core.Logging;
using Serilog;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Logging;

/// <summary>
/// <see cref="ISecurityLogger{TCategoryName}"/> implementation that writes to the dedicated
/// <see cref="SecuritySerilogLogger"/> with <typeparamref name="TCategoryName"/> as the source context.
/// </summary>
public sealed class SecurityLogger<TCategoryName> : ISecurityLogger<TCategoryName>
{
    private readonly ILogger _logger;

    public SecurityLogger(SecuritySerilogLogger securityLogger) =>
        _logger = securityLogger.Logger.ForContext<TCategoryName>();

    public void LogSecurityDebug(string messageTemplate, params object?[] propertyValues) =>
        _logger.Debug(messageTemplate, propertyValues);

    public void LogSecurityInformation(string messageTemplate, params object?[] propertyValues) =>
        _logger.Information(messageTemplate, propertyValues);

    public void LogSecurityWarning(string messageTemplate, params object?[] propertyValues) =>
        _logger.Warning(messageTemplate, propertyValues);

    public void LogSecurityWarning(Exception exception, string messageTemplate, params object?[] propertyValues) =>
        _logger.Warning(exception, messageTemplate, propertyValues);

    public void LogSecurityError(string messageTemplate, params object?[] propertyValues) =>
        _logger.Error(messageTemplate, propertyValues);

    public void LogSecurityError(Exception exception, string messageTemplate, params object?[] propertyValues) =>
        _logger.Error(exception, messageTemplate, propertyValues);
}
