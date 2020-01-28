using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using Serilog.Core;
using Serilog.Core.Enrichers;

namespace Enigmatry.Blueprint.Api.Infrastructure.Logging
{
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

        private ILogEventEnricher[] CreateEnrichers(HttpContext context)
        {
            return new ILogEventEnricher[]
            {
                new PropertyEnricher("User", context.User.Identity.Name ?? String.Empty),
                new PropertyEnricher("Address", context.Connection.RemoteIpAddress)
            };
        }
    }
}
