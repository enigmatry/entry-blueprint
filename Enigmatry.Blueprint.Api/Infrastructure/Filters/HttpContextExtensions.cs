using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Blueprint.Api.Infrastructure.Filters
{
    public static class HttpContextExtensions
    {
        public static T Resolve<T>(this HttpContext httpContext)
        {
            return httpContext.RequestServices.GetService<T>();
        }
    }
}