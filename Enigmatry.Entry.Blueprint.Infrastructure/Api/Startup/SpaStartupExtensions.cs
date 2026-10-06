using System.Text.RegularExpressions;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Startup;

public static partial class SpaStartupExtensions
{
    private const string IndexHtml = "index.html";

    // Matches the content hash the Angular application builder appends with outputHashing "all",
    // e.g. main-A1B2C3D4.js. Files copied via the angular.json "assets" option (favicon.ico, assets/**)
    // keep stable names and must not be cached long-lived.
    [GeneratedRegex(@"-[A-Z0-9]{8}\.[a-z0-9]+$")]
    private static partial Regex HashedFileNameRegex();

    // Matches the opening of a stylesheet <link> regardless of attribute order, skipping tags that already carry a nonce.
    [GeneratedRegex(@"<link\s(?![^>]*\bnonce=)(?=[^>]*\brel=""stylesheet"")", RegexOptions.IgnoreCase)]
    private static partial Regex StylesheetLinkRegex();

    /// <summary>
    /// Serves the built Angular app from wwwroot with long-lived caching for hashed bundles. index.html is hidden
    /// from the static-file middleware so that the only way to the SPA shell is the nonce-injecting fallback; the
    /// canonical /index.html is rewritten to "/" to keep direct navigation working.
    /// </summary>
    public static void AppUseSpaStaticFiles(this WebApplication app)
    {
        app.Use((context, next) =>
        {
            if (context.Request.Path.Equals("/" + IndexHtml, StringComparison.OrdinalIgnoreCase))
            {
                context.Request.Path = "/";
            }

            return next(context);
        });

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new IndexHtmlHidingFileProvider(app.Environment.WebRootFileProvider),
            OnPrepareResponse = context =>
            {
                if (IsHashedFileName(context.File.Name))
                {
                    context.Context.Response.Headers.CacheControl = "public, max-age=604800, immutable";
                }
            }
        });
    }

    /// <summary>
    /// Serves wwwroot/index.html for unmatched non-API document requests (Angular deep links), injecting the
    /// per-request CSP nonce. Paths with a file extension are asset requests (favicon, mistyped bundle names)
    /// and get a real 404 instead of a 200 with the shell.
    /// </summary>
    public static void AppMapSpaFallback(this WebApplication app) =>
        app.MapFallback(async context =>
        {
            if (context.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase) ||
                context.Request.Path.StartsWithSegments("/swagger", StringComparison.OrdinalIgnoreCase) ||
                Path.HasExtension(context.Request.Path.Value))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            var environment = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
            var file = environment.WebRootFileProvider.GetFileInfo(IndexHtml);
            if (!file.Exists) // local development or test host without a built SPA in wwwroot
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            var nonce = context.RequestServices.GetRequiredService<NonceProvider>().Nonce;
            using var reader = new StreamReader(file.CreateReadStream());
            var html = await reader.ReadToEndAsync(context.RequestAborted);

            context.Response.ContentType = "text/html; charset=utf-8";
            context.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
            await context.Response.WriteAsync(InjectNonce(html, nonce), context.RequestAborted);
        }).AllowAnonymous();

    internal static string InjectNonce(string html, string nonce) =>
        StylesheetLinkRegex()
            .Replace(html, $"<link nonce=\"{nonce}\" ")
            .Replace(CspConstants.NoncePlaceholder, nonce, StringComparison.Ordinal);

    internal static bool IsHashedFileName(string fileName) => HashedFileNameRegex().IsMatch(fileName);
}
