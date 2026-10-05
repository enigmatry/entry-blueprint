namespace Enigmatry.Entry.Blueprint.Core.Logging;

/// <summary>
/// Abstraction for security and audit logging. Events written through this interface end up in the dedicated
/// security log (configured in the <c>SecuritySerilog</c> section) instead of the regular application log.
/// Generic over the consuming type so the class name surfaces as <c>{SourceContext}</c> in the security log output.
/// </summary>
/// <typeparam name="TCategoryName">The type whose name is used as the log category.</typeparam>
/// <example>
/// <code>
/// public class PermissionHandler(ISecurityLogger&lt;PermissionHandler&gt; logger)
/// {
///     public void Deny(Guid userId) =>
///         logger.LogSecurityWarning("User {UserId} was denied access.", userId);
/// }
/// </code>
/// </example>
// ReSharper disable once UnusedTypeParameter
public interface ISecurityLogger<out TCategoryName>
{
    /// <summary>Writes a debug level security event.</summary>
    public void LogSecurityDebug(string messageTemplate, params object?[] propertyValues);

    /// <summary>Writes an information level security event.</summary>
    public void LogSecurityInformation(string messageTemplate, params object?[] propertyValues);

    /// <summary>Writes a warning level security event.</summary>
    public void LogSecurityWarning(string messageTemplate, params object?[] propertyValues);

    /// <summary>Writes a warning level security event including the exception.</summary>
    public void LogSecurityWarning(Exception exception, string messageTemplate, params object?[] propertyValues);

    /// <summary>Writes an error level security event.</summary>
    public void LogSecurityError(string messageTemplate, params object?[] propertyValues);

    /// <summary>Writes an error level security event including the exception.</summary>
    public void LogSecurityError(Exception exception, string messageTemplate, params object?[] propertyValues);
}
