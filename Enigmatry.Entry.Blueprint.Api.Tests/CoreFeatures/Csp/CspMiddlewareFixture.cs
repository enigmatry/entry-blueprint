using Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Shouldly;

namespace Enigmatry.Entry.Blueprint.Api.Tests.CoreFeatures.Csp;

[Category("unit")]
public class CspMiddlewareFixture
{
    private const string StrictCsp = "default-src 'none'; frame-ancestors 'none'";
    private const string SpaCsp = "default-src 'none'; style-src 'nonce-**PLACEHOLDER_NONCE_SERVER**'; script-src 'self' 'nonce-**PLACEHOLDER_NONCE_SERVER**'";

    [Test]
    public async Task GivenApiPath_StrictPolicyIsSet()
    {
        var context = await InvokeAsync("/api/users", SpaCsp, out _);

        GetCsp(context).ShouldBe(StrictCsp);
    }

    [Test]
    public async Task GivenSpaPath_ConfiguredSpaPolicyWithNonceIsSet()
    {
        var context = await InvokeAsync("/users/42", SpaCsp, out var nonce);

        GetCsp(context).ShouldBe($"default-src 'none'; style-src 'nonce-{nonce}'; script-src 'self' 'nonce-{nonce}'");
    }

    [Test]
    public async Task GivenSwaggerPath_NoHeaderIsSet()
    {
        // Swagger UI only runs in local development and needs inline scripts and styles; it is exempt from CSP.
        var context = await InvokeAsync("/swagger/index.html", SpaCsp, out _);

        context.Response.Headers.ContainsKey(CspConstants.HeaderName).ShouldBeFalse();
    }

    [Test]
    public async Task GivenSpaPathWithoutConfiguredPolicy_StrictPolicyIsSet()
    {
        var context = await InvokeAsync("/users/42", spaCsp: null, out _);

        GetCsp(context).ShouldBe(StrictCsp);
    }

    [TestCase("")]
    [TestCase("   ")]
    public async Task GivenEmptySpaPolicy_StrictPolicyIsSet(string emptyPolicy)
    {
        // A Web Deploy parameter left at its empty default must not disable the CSP.
        var context = await InvokeAsync("/", emptyPolicy, out _);

        GetCsp(context).ShouldBe(StrictCsp);
    }

    [Test]
    public void GivenConfiguredPolicyWithoutPlaceholder_ConstructionFails()
    {
        // Such a policy can never carry the nonce, so fail at startup instead of shipping it.
        var configuration = BuildConfiguration("default-src 'self'");

        Should.Throw<InvalidOperationException>(() => new CspMiddleware(_ => Task.CompletedTask, configuration))
            .Message.ShouldContain(CspConstants.SpaSettingsName);
    }

    [Test]
    public async Task GivenApiPathWithDifferentCasing_StrictPolicyIsSet()
    {
        var context = await InvokeAsync("/API/Users", SpaCsp, out _);

        GetCsp(context).ShouldBe(StrictCsp);
    }

    [Test]
    public async Task Middleware_CallsNext()
    {
        var nextCalled = false;
        var middleware = new CspMiddleware(_ =>
        {
            nextCalled = true;
            return Task.CompletedTask;
        }, BuildConfiguration(SpaCsp));

        await middleware.InvokeAsync(new DefaultHttpContext { Request = { Path = "/" } }, new NonceProvider());

        nextCalled.ShouldBeTrue();
    }

    [Test]
    public async Task GivenResponseClearedBeforeItStarts_HeaderIsReappliedWithTheSameNonce()
    {
        // The exception handler calls Response.Clear() before writing ProblemDetails, which drops the header.
        var responseFeature = new OnStartingRecordingResponseFeature();
        var context = new DefaultHttpContext();
        context.Features.Set<IHttpResponseFeature>(responseFeature);
        context.Request.Path = "/users/42";
        var nonceProvider = new NonceProvider();
        var middleware = new CspMiddleware(_ => Task.CompletedTask, BuildConfiguration(SpaCsp));

        await middleware.InvokeAsync(context, nonceProvider);
        context.Response.Clear();
        await responseFeature.RunOnStartingCallbacksAsync();

        GetCsp(context).ShouldContain($"'nonce-{nonceProvider.Nonce}'");
    }

    private static Task<HttpContext> InvokeAsync(string path, string? spaCsp, out string nonce)
    {
        var nonceProvider = new NonceProvider();
        nonce = nonceProvider.Nonce;
        return InvokeAsync(path, spaCsp, nonceProvider);
    }

    private static async Task<HttpContext> InvokeAsync(string path, string? spaCsp, NonceProvider nonceProvider)
    {
        var context = new DefaultHttpContext { Request = { Path = path } };
        var middleware = new CspMiddleware(_ => Task.CompletedTask, BuildConfiguration(spaCsp));

        await middleware.InvokeAsync(context, nonceProvider);

        return context;
    }

    private static string GetCsp(HttpContext context) => context.Response.Headers[CspConstants.HeaderName].ToString();

    private static IConfiguration BuildConfiguration(string? spaCsp) =>
        new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?> { { CspConstants.SpaSettingsName, spaCsp } })
            .Build();

    // DefaultHttpContext's response feature discards OnStarting callbacks; this one records them so a test can run them.
    private sealed class OnStartingRecordingResponseFeature : HttpResponseFeature
    {
        private readonly List<(Func<object, Task> Callback, object State)> _callbacks = [];

        public override void OnStarting(Func<object, Task> callback, object state) => _callbacks.Add((callback, state));

        public async Task RunOnStartingCallbacksAsync()
        {
            foreach (var (callback, state) in _callbacks)
            {
                await callback(state);
            }
        }
    }
}
