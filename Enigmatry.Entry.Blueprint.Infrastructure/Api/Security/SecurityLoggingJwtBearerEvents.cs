using Enigmatry.Entry.Blueprint.Core.Logging;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Security;

/// <summary>
/// JWT bearer events that write authentication outcomes (validated tokens and authentication failures) to the
/// security log. The logger is resolved per request because the events instance is created once at startup.
/// </summary>
[UsedImplicitly]
public class SecurityLoggingJwtBearerEvents : JwtBearerEvents
{
    public override Task TokenValidated(TokenValidatedContext context)
    {
        ResolveLogger(context).LogSecurityDebug("Token validated for {UserName} from {RemoteIpAddress}.",
            context.Principal?.Identity?.Name, context.HttpContext.Connection.RemoteIpAddress);
        return base.TokenValidated(context);
    }

    public override Task AuthenticationFailed(AuthenticationFailedContext context)
    {
        ResolveLogger(context).LogSecurityWarning(context.Exception,
            "Authentication failed for request to {Method} {Path} from {RemoteIpAddress}: {Reason}.",
            context.Request.Method, context.Request.Path.Value, context.HttpContext.Connection.RemoteIpAddress,
            context.Exception.Message);
        return base.AuthenticationFailed(context);
    }

    private static ISecurityLogger<SecurityLoggingJwtBearerEvents> ResolveLogger(ResultContext<JwtBearerOptions> context) =>
        context.HttpContext.RequestServices.GetRequiredService<ISecurityLogger<SecurityLoggingJwtBearerEvents>>();
}
