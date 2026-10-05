using Enigmatry.Entry.Blueprint.Core.Logging;
using Enigmatry.Entry.Blueprint.Domain.Identity;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Authorization;

/// <summary>
/// Wraps the default <see cref="AuthorizationMiddlewareResultHandler"/> and writes every challenged (401) and
/// forbidden (403) authorization outcome to the security log before the default response is produced.
/// </summary>
[UsedImplicitly]
public class SecurityLoggingAuthorizationMiddlewareResultHandler(
    ISecurityLogger<SecurityLoggingAuthorizationMiddlewareResultHandler> logger) : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

    public Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Challenged)
        {
            logger.LogSecurityWarning("Unauthenticated request to {Method} {Path} from {RemoteIpAddress} was challenged.",
                context.Request.Method, context.Request.Path.Value, context.Connection.RemoteIpAddress);
        }
        else if (authorizeResult.Forbidden)
        {
            var failedRequirements = authorizeResult.AuthorizationFailure?.FailedRequirements.Select(r => r.ToString()) ?? [];
            logger.LogSecurityWarning(
                "User {UserId} ({UserName}) was denied access to {Method} {Path} from {RemoteIpAddress}. Failed requirements: {FailedRequirements}.",
                GetCurrentUserId(context), context.User.Identity?.Name, context.Request.Method, context.Request.Path.Value,
                context.Connection.RemoteIpAddress, String.Join(", ", failedRequirements));
        }

        return _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }

    private static Guid? GetCurrentUserId(HttpContext context) =>
        context.RequestServices.GetService<ICurrentUserProvider>()?.UserId;
}
