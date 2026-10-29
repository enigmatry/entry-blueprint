using Enigmatry.Entry.Blueprint.Infrastructure.Api.Startup;
using Microsoft.Extensions.FileProviders;
using Shouldly;

namespace Enigmatry.Entry.Blueprint.Api.Tests.CoreFeatures.Csp;

[Category("unit")]
public class IndexHtmlHidingFileProviderFixture
{
    private string _root = null!;

    [SetUp]
    public void SetUp()
    {
        _root = Path.Combine(TestContext.CurrentContext.WorkDirectory, "wwwroot-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_root);
        File.WriteAllText(Path.Combine(_root, "index.html"), "<html></html>");
        File.WriteAllText(Path.Combine(_root, "main-A1B2C3D4.js"), "console.log(1);");
    }

    [TearDown]
    public void TearDown() => Directory.Delete(_root, recursive: true);

    [TestCase("/index.html")]
    [TestCase("index.html")]
    [TestCase("//index.html")]
    [TestCase("\\index.html")]
    [TestCase("/INDEX.HTML")]
    public void GivenIndexHtmlPath_FileIsReportedAsMissing(string subpath)
    {
        using var physical = new PhysicalFileProvider(_root);
        var provider = new IndexHtmlHidingFileProvider(physical);

        provider.GetFileInfo(subpath).Exists.ShouldBeFalse();
    }

    [Test]
    public void GivenOtherFile_FileIsServed()
    {
        using var physical = new PhysicalFileProvider(_root);
        var provider = new IndexHtmlHidingFileProvider(physical);

        provider.GetFileInfo("/main-A1B2C3D4.js").Exists.ShouldBeTrue();
    }
}
