using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Startup;

// PhysicalFileProvider trims leading separators, so without this //index.html would serve the raw shell with the
// nonce placeholder unsubstituted; the shell must only come from the nonce-injecting fallback.
internal sealed class IndexHtmlHidingFileProvider(IFileProvider inner) : IFileProvider
{
    public IFileInfo GetFileInfo(string subpath) =>
        IsIndexHtml(subpath) ? new NotFoundFileInfo(subpath) : inner.GetFileInfo(subpath);

    public IDirectoryContents GetDirectoryContents(string subpath) => inner.GetDirectoryContents(subpath);

    public IChangeToken Watch(string filter) => inner.Watch(filter);

    private static bool IsIndexHtml(string subpath) =>
        subpath.TrimStart('/', '\\').Equals("index.html", StringComparison.OrdinalIgnoreCase);
}
