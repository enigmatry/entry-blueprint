using Enigmatry.Entry.AspNetCore.Filters;
using Enigmatry.Entry.Blueprint.Domain.Identity;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using Serilog.Core;
using Serilog.Core.Enrichers;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Logging;

[UsedImplicitly]
public class LogContextMiddleware
{
    private readonly RequestDelegate _next;

    public LogContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    [UsedImplicitly]
    public async Task InvokeAsync(HttpContext context)
    {
        using (LogContext.Push(CreateEnrichers(context)))
        {
            await _next.Invoke(context);
        }
    }

    private static ILogEventEnricher[] CreateEnrichers(HttpContext context) => [new PropertyEnricher("User", GetCurrentUserId(context)!), new PropertyEnricher("Address", context.Connection.RemoteIpAddress!)
    ];

    private static Guid? GetCurrentUserId(HttpContext context)
    {
        var userId = context.Resolve<ICurrentUserProvider>().User?.UserId;
        return userId;
    }
}
