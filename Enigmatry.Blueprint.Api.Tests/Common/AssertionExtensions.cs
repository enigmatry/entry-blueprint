using System.Net.Http;

namespace Enigmatry.Blueprint.Api.Tests.Common
{
    public static class AssertionExtensions
    {
        public static HttpResponseAssertions Should(this HttpResponseMessage actualValue)
        {
            return new HttpResponseAssertions(actualValue);
        }
    }
}