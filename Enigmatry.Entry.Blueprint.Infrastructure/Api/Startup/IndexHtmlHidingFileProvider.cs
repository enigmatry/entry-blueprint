using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Startup;

/// <summary>
/// Hides wwwroot/index.html from the static-file middleware. PhysicalFileProvider trims all leading path
/// separators, so non-canonical request paths such as //index.html would otherwise serve the raw file with the
/// CSP nonce placeholder unsubstituted. The SPA shell must only be served by the nonce-injecting fallback.
/// </summary>
internal sealed class IndexHtmlHidingFileProvider(IFileProvider inner) : IFileProvider
{
    public IFileInfo GetFileInfo(string subpath) =>
        IsIndexHtml(subpath) ? new NotFoundFileInfo(subpath) : inner.GetFileInfo(subpath);

    public IDirectoryContents GetDirectoryContents(string subpath) => inner.GetDirectoryContents(subpath);

    public IChangeToken Watch(string filter) => inner.Watch(filter);

    private static bool IsIndexHtml(string subpath) =>
        subpath.TrimStart('/', '\\').Equals("index.html", StringComparison.OrdinalIgnoreCase);
}
