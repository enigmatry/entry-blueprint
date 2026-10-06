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

    // Angular output hashing, e.g. main-A1B2C3D4.js; assets copied via angular.json keep stable names and must not be cached long-lived.
    [GeneratedRegex(@"-[A-Z0-9]{8}\.[a-z0-9]+$")]
    private static partial Regex HashedFileNameRegex();

    [GeneratedRegex(@"<link\s(?![^>]*\bnonce=)(?=[^>]*\brel=""stylesheet"")", RegexOptions.IgnoreCase)]
    private static partial Regex StylesheetLinkRegex();

    public static void AppUseSpaStaticFiles(this WebApplication app)
    {
        // index.html is hidden from static files below, so route direct navigation to it through the fallback.
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

    public static void AppMapSpaFallback(this WebApplication app) =>
        app.MapFallback(async context =>
        {
            // Paths with an extension are asset requests (e.g. a mistyped bundle name) and get a real 404, not the shell.
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
