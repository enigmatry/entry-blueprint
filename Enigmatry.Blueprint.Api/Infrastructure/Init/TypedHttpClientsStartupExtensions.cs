using System;
using System.Net.Http;
using Enigmatry.Blueprint.Api.Features.GitHubApi;
using Enigmatry.Blueprint.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;

namespace Enigmatry.Blueprint.Api.Infrastructure.Init
{
    public static class TypedHttpClientsStartupExtensions
    {
        public static void AppAddTypedHttpClients(this IServiceCollection services, AppSettings settings)
        {
            GitHubApiSettings gitHubApiSettings = settings.GitHubApi;
            services.AddHttpClient("GitHub", options =>
                {
                    options.BaseAddress = new Uri(gitHubApiSettings.BaseUrl);
                    options.Timeout = gitHubApiSettings.Timeout;
                    options.DefaultRequestHeaders.Add("User-Agent", "request"); // needed to call GitHub API
                })
                // these are some examples of policies, not all are needed (e.g. both timeout policies)
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(gitHubApiSettings.Timeout))
                .AddPolicyHandlerFromRegistry(PollyStartupExtensions.GlobalTimeoutPolicyName)
                // Handle 5xx status code and any responses with a 408 (Request Timeout) status code,
                // see: https://github.com/App-vNext/Polly/wiki/Polly-and-HttpClientFactory#using-addtransienthttperrorpolicy
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddTypedClient(RestService.For<IGitHubClient>);
        }
    }
}
