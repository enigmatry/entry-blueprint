using System.Net;
using System.Text.RegularExpressions;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Api;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;
using Shouldly;

namespace Enigmatry.Entry.Blueprint.Api.Tests.CoreFeatures.Csp;

[Category("integration")]
public partial class SecurityHeadersFixture : IntegrationFixtureBase
{
    private const string StrictCsp = "default-src 'none'; frame-ancestors 'none'";

    [GeneratedRegex(@"'nonce-([A-Za-z0-9+/=]+)'")]
    private static partial Regex NonceRegex();

    [Test]
    public async Task ApiResponse_HasStrictCsp()
    {
        var response = await Client.GetAsync("api/users");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        GetCsp(response).ShouldBe(StrictCsp);
    }

    [Test]
    public async Task UnknownApiPath_Returns404AndNotTheSpaShell()
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

    [Test]
    public async Task SpaPath_HasConfiguredPolicyWithNonceSubstituted()
    {
        var response = await Client.GetAsync("users/42");

        var csp = GetCsp(response);
        csp.ShouldNotContain(CspConstants.NoncePlaceholder);
        csp.ShouldContain("base-uri 'self'");
        var nonces = NonceRegex().Matches(csp).Select(m => m.Groups[1].Value).Distinct().ToList();
        nonces.Count.ShouldBe(1, "script-src and style-src must share the request nonce");
        Convert.FromBase64String(nonces[0]).Length.ShouldBe(32);
    }

    [Test]
    public async Task SpaPath_NonceDiffersPerRequest()
    {
        var first = NonceRegex().Match(GetCsp(await Client.GetAsync("users/42"))).Groups[1].Value;
        var second = NonceRegex().Match(GetCsp(await Client.GetAsync("users/42"))).Groups[1].Value;

        first.ShouldNotBe(second);
    }

    [Test]
    public async Task SpaShell_NeverContainsThePlaceholder()
    {
        // The test host has no built SPA in wwwroot, so the fallback answers 404. If a developer has copied a
        // build into wwwroot, the shell is served and must carry the substituted nonce instead.
        foreach (var path in new[] { "", "index.html", "//index.html" })
        {
            var response = await Client.GetAsync(path);
            var body = await response.Content.ReadAsStringAsync();

            body.ShouldNotContain(CspConstants.NoncePlaceholder);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var nonce = NonceRegex().Match(GetCsp(response)).Groups[1].Value;
                body.ShouldContain($"ngCspNonce=\"{nonce}\"");
            }
            else
            {
                response.StatusCode.ShouldBe(HttpStatusCode.NotFound, $"path '{path}'");
            }
        }
    }

    private static string GetCsp(HttpResponseMessage response) =>
        response.Headers.GetValues(CspConstants.HeaderName).Single();
}
