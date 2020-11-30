﻿using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.Api
{
    public static class ServiceScopeExtensions
    {
        public static T Resolve<T>(this IServiceScope scope) => scope.ServiceProvider.GetRequiredService<T>();
    }
}
