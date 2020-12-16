using Microsoft.AspNetCore.Http;

namespace Enigmatry.Blueprint.Infrastructure.Api.Filters
{
    public static class HttpRequestExtensions
    {
        public static bool IsTrusted(this HttpRequest request, bool useDeveloperExceptionPage)
        {
            // TODO add logic if for certain users/roles you want to return error details in the response
            return useDeveloperExceptionPage;
        }
    }
}