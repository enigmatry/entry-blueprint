using System.Net;
using System.Text.RegularExpressions;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Api;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;
using Shouldly;

namespace Enigmatry.Entry.Blueprint.Api.Tests.CoreFeatures.Csp;

// The SPA shell comes from the test web root configured in ApiWebApplicationFactory (TestWebRoot/).
[Category("integration")]
public partial class SecurityHeadersFixture : IntegrationFixtureBase
{
    private const string StrictCsp = "default-src 'none'; frame-ancestors 'none'";

    [GeneratedRegex(@"'nonce-([A-Za-z0-9+/=]+)'")]
    private static partial Regex NonceRegex();

    [Test]
    public async Task UnknownApiPath_Returns404WithStrictCsp()
    {
        var response = await Client.GetAsync("api/does-not-exist");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        response.Content.Headers.ContentType?.MediaType.ShouldNotBe("text/html");
        GetCsp(response).ShouldBe(StrictCsp);
    }

    [Test]
    public async Task PathWithExtension_Returns404()
    {
        // Accepted trade-off: a deep link whose last segment contains a dot is treated as an asset request.
        var response = await Client.GetAsync("users/john.doe");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [TestCase("")]
    [TestCase("index.html")]
    [TestCase("users/42")]
    public async Task SpaShell_IsServedWithTheNonceSubstituted(string path)
    {
        var response = await Client.GetAsync(path);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        response.Content.Headers.ContentType?.MediaType.ShouldBe("text/html");
        response.Headers.CacheControl?.NoStore.ShouldBe(true);
        var csp = GetCsp(response);
        csp.ShouldNotContain(CspConstants.NoncePlaceholder);
        csp.ShouldContain("base-uri 'self'");
        var nonces = NonceRegex().Matches(csp).Select(m => m.Groups[1].Value).Distinct().ToList();
        nonces.Count.ShouldBe(1, "script-src and style-src must share the request nonce");
        Convert.FromBase64String(nonces[0]).Length.ShouldBe(32);
        var body = await response.Content.ReadAsStringAsync();
        body.ShouldNotContain(CspConstants.NoncePlaceholder);
        body.ShouldContain($"ngCspNonce=\"{nonces[0]}\"");
        body.ShouldContain($"<link nonce=\"{nonces[0]}\" rel=\"stylesheet\"");
    }

    [Test]
    public async Task SpaPath_NonceDiffersPerRequest()
    {
        var first = NonceRegex().Match(GetCsp(await Client.GetAsync("users/42"))).Groups[1].Value;
        var second = NonceRegex().Match(GetCsp(await Client.GetAsync("users/42"))).Groups[1].Value;

        first.ShouldNotBe(second);
    }

    [Test]
    public async Task NonCanonicalIndexHtmlPath_IsNotServedRaw()
    {
        // "//index.html" as a relative URI would resolve to a host, so build the absolute URI by hand.
        var response = await Client.GetAsync(new Uri(Client.BaseAddress + "/index.html"));

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task HashedBundle_IsServedWithImmutableCacheAndCsp()
    {
        var response = await Client.GetAsync("main-A1B2C3D4.js");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        response.Headers.CacheControl?.ToString().ShouldContain("immutable");
        GetCsp(response).ShouldContain("'nonce-");
    }

    [Test]
    public async Task SwaggerUi_IsServedUnderSwaggerPathWithNoncedTags()
    {
        // The test host runs as Development, where Swagger UI is enabled.
        var response = await Client.GetAsync("swagger/index.html");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        response.Content.Headers.ContentType?.MediaType.ShouldBe("text/html");
        var csp = GetCsp(response);
        csp.ShouldNotContain(CspConstants.NoncePlaceholder);
        var nonce = NonceRegex().Match(csp).Groups[1].Value;
        nonce.ShouldNotBeNullOrEmpty();
        var body = await response.Content.ReadAsStringAsync();
        var scriptTags = Regex.Matches(body, "<script[^>]*>", RegexOptions.IgnoreCase).Select(m => m.Value).ToList();
        scriptTags.ShouldNotBeEmpty();
        scriptTags.ShouldAllBe(tag => tag.Contains($"nonce=\"{nonce}\"", StringComparison.Ordinal));
    }

    private static string GetCsp(HttpResponseMessage response) =>
        response.Headers.GetValues(CspConstants.HeaderName).Single();
}
