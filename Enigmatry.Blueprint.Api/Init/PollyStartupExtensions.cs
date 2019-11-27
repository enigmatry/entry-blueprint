using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Registry;
using Polly.Timeout;

namespace Enigmatry.Blueprint.Api.Init
{
    public static class PollyStartupExtensions
    {
        public const string GlobalTimeoutPolicyName = "global-timeout";

        public static void AppAddPolly(this IServiceCollection services)
        {
            // Add registry
            IPolicyRegistry<string> policyRegistry = services.AddPolicyRegistry();

            // Centrally stored policies
            AsyncTimeoutPolicy<HttpResponseMessage> timeoutPolicy =
                Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMilliseconds(1500));
            policyRegistry.Add(GlobalTimeoutPolicyName, timeoutPolicy);
        }
    }
}
