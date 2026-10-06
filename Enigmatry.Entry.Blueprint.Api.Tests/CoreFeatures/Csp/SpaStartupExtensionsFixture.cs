using Enigmatry.Entry.Blueprint.Infrastructure.Api.Startup;
using Shouldly;

namespace Enigmatry.Entry.Blueprint.Api.Tests.CoreFeatures.Csp;

[Category("unit")]
public class SpaStartupExtensionsFixture
{
    private const string Nonce = "test-nonce";

    [Test]
    public void GivenStylesheetLinkWithRelFirst_WhenInjectingNonce_NonceIsAdded()
    {
        var html = "<link rel=\"stylesheet\" href=\"styles-A1B2C3D4.css\">";

        var result = SpaStartupExtensions.InjectNonce(html, Nonce);

        result.ShouldBe($"<link nonce=\"{Nonce}\" rel=\"stylesheet\" href=\"styles-A1B2C3D4.css\">");
    }

    [Test]
    public void GivenStylesheetLinkWithHrefFirst_WhenInjectingNonce_NonceIsAdded()
    {
        var html = "<link href=\"https://fonts.googleapis.com/icon?family=Material+Icons\" rel=\"stylesheet\">";

        var result = SpaStartupExtensions.InjectNonce(html, Nonce);

        result.ShouldBe($"<link nonce=\"{Nonce}\" href=\"https://fonts.googleapis.com/icon?family=Material+Icons\" rel=\"stylesheet\">");
    }

    [Test]
    public void GivenNonStylesheetLink_WhenInjectingNonce_LinkIsUnchanged()
    {
        var html = "<link rel=\"icon\" type=\"image/x-icon\" href=\"favicon.ico\">";

        var result = SpaStartupExtensions.InjectNonce(html, Nonce);

        result.ShouldBe(html);
    }

    [Test]
    public void GivenStylesheetLinkThatAlreadyHasNonce_WhenInjectingNonce_NonceIsNotDuplicated()
    {
        var html = "<link nonce=\"existing\" rel=\"stylesheet\" href=\"styles.css\">";

        var result = SpaStartupExtensions.InjectNonce(html, Nonce);

        result.ShouldBe(html);
    }

    [Test]
    public void GivenNoncePlaceholder_WhenInjectingNonce_PlaceholderIsReplaced()
    {
        var html = "<app-root ngCspNonce=\"**PLACEHOLDER_NONCE_SERVER**\"></app-root>";

        var result = SpaStartupExtensions.InjectNonce(html, Nonce);

        result.ShouldBe($"<app-root ngCspNonce=\"{Nonce}\"></app-root>");
    }

    [Test]
    public void GivenFullIndexHtml_WhenInjectingNonce_EveryStylesheetAndThePlaceholderGetTheNonce()
    {
        var html = """
            <head>
              <link rel="icon" type="image/x-icon" href="favicon.ico">
              <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
              <link rel="stylesheet" href="styles-A1B2C3D4.css">
            </head>
            <body><app-root ngCspNonce="**PLACEHOLDER_NONCE_SERVER**" class="root-container"></app-root></body>
            """;

        var result = SpaStartupExtensions.InjectNonce(html, Nonce);

        result.ShouldNotContain("**PLACEHOLDER_NONCE_SERVER**");
        result.Split($"<link nonce=\"{Nonce}\" ").Length.ShouldBe(2 + 1); // two stylesheet links
        result.ShouldContain($"ngCspNonce=\"{Nonce}\"");
        result.ShouldContain("<link rel=\"icon\" type=\"image/x-icon\" href=\"favicon.ico\">");
    }

    [TestCase("main-A1B2C3D4.js", true)]
    [TestCase("styles-54GOK4WX.css", true)]
    [TestCase("chunk-UHOORSFW.js", true)]
    [TestCase("favicon.ico", false)]
    [TestCase("index.html", false)]
    [TestCase("logo-small.png", false)]
    [TestCase("main-a1b2c3d4.js", false)]
    public void IsHashedFileName_MatchesAngularOutputHashingOnly(string fileName, bool expected)
    {
        SpaStartupExtensions.IsHashedFileName(fileName).ShouldBe(expected);
    }
}
