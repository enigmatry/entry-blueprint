using Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shouldly;

namespace Enigmatry.Entry.Blueprint.Api.Tests.CoreFeatures.Csp;

[Category("unit")]
public class CspMiddlewareFixture
{
    private const string StrictCsp = "default-src 'none'; frame-ancestors 'none'";
    private const string SpaCsp = "default-src 'none'; style-src 'nonce-**PLACEHOLDER_NONCE_SERVER**'; script-src 'self' 'nonce-**PLACEHOLDER_NONCE_SERVER**'";
    private const string SwaggerCsp = "default-src 'none'; script-src 'nonce-**PLACEHOLDER_NONCE_SERVER**'";

    [Test]
    public async Task GivenApiPath_StrictPolicyIsSet()
    {
        var header = await InvokeAsync("/api/users", SpaCsp, SwaggerCsp, out _);

        header.ShouldBe(StrictCsp);
    }

    [Test]
    public async Task GivenSpaPath_ConfiguredSpaPolicyWithNonceIsSet()
    {
        var header = await InvokeAsync("/users/42", SpaCsp, SwaggerCsp, out var nonce);

        header.ShouldBe($"default-src 'none'; style-src 'nonce-{nonce}'; script-src 'self' 'nonce-{nonce}'");
    }

    [Test]
    public async Task GivenRootPath_ConfiguredSpaPolicyIsSet()
    {
        var header = await InvokeAsync("/", SpaCsp, SwaggerCsp, out var nonce);

        header.ShouldContain($"'nonce-{nonce}'");
        header.ShouldNotContain(CspConstants.NoncePlaceholder);
    }

    [Test]
    public async Task GivenSwaggerPath_ConfiguredSwaggerPolicyWithNonceIsSet()
    {
        var header = await InvokeAsync("/swagger/index.html", SpaCsp, SwaggerCsp, out var nonce);

        header.ShouldBe($"default-src 'none'; script-src 'nonce-{nonce}'");
    }

    [Test]
    public async Task GivenSpaPathWithoutConfiguredPolicy_StrictPolicyIsSet()
    {
        var header = await InvokeAsync("/users/42", spaCsp: null, swaggerCsp: null, out _);

        header.ShouldBe(StrictCsp);
    }

    [Test]
    public async Task GivenSwaggerPathWithoutConfiguredPolicy_StrictPolicyIsSet()
    {
        var header = await InvokeAsync("/swagger/index.html", spaCsp: null, swaggerCsp: null, out _);

        header.ShouldBe(StrictCsp);
    }

    [Test]
    public async Task GivenSpaPolicyWithoutPlaceholder_ValueIsSentUnchanged()
    {
        const string policyWithoutPlaceholder = "default-src 'self'";

        var header = await InvokeAsync("/", policyWithoutPlaceholder, swaggerCsp: null, out _);

        header.ShouldBe(policyWithoutPlaceholder);
    }

    [Test]
    public async Task GivenApiPathWithDifferentCasing_StrictPolicyIsSet()
    {
        var header = await InvokeAsync("/API/Users", SpaCsp, SwaggerCsp, out _);

        header.ShouldBe(StrictCsp);
    }

    [Test]
    public async Task Middleware_CallsNext()
    {
        var nextCalled = false;
        var middleware = new CspMiddleware(_ =>
        {
            nextCalled = true;
            return Task.CompletedTask;
        }, BuildConfiguration(SpaCsp, SwaggerCsp));
        using var nonceProvider = new NonceProvider();

        await middleware.InvokeAsync(new DefaultHttpContext { Request = { Path = "/" } }, nonceProvider);

        nextCalled.ShouldBeTrue();
    }

    private static Task<string> InvokeAsync(string path, string? spaCsp, string? swaggerCsp, out string nonce)
    {
        var nonceProvider = new NonceProvider();
        nonce = nonceProvider.Nonce;
        return InvokeAsync(path, spaCsp, swaggerCsp, nonceProvider);
    }

    private static async Task<string> InvokeAsync(string path, string? spaCsp, string? swaggerCsp, NonceProvider nonceProvider)
    {
        using (nonceProvider)
        {
            var context = new DefaultHttpContext { Request = { Path = path } };
            var middleware = new CspMiddleware(_ => Task.CompletedTask, BuildConfiguration(spaCsp, swaggerCsp));

            await middleware.InvokeAsync(context, nonceProvider);

            return context.Response.Headers[CspConstants.HeaderName].ToString();
        }
    }

    private static IConfiguration BuildConfiguration(string? spaCsp, string? swaggerCsp) =>
        new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { CspConstants.SpaSettingsName, spaCsp },
                { CspConstants.SettingsName, swaggerCsp }
            })
            .Build();
}
