using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;

[UsedImplicitly]
public partial class SwaggerNonceMiddleware(RequestDelegate next)
{
    [GeneratedRegex(@"<script([^>]*?)>", RegexOptions.IgnoreCase | RegexOptions.Singleline)]
    private static partial Regex ScriptTagRegex();

    [GeneratedRegex(@"<style([^>]*?)>", RegexOptions.IgnoreCase | RegexOptions.Singleline)]
    private static partial Regex StyleTagRegex();

    [UsedImplicitly]
    public async Task InvokeAsync(HttpContext context, NonceProvider nonceProvider)
    {
        if (!context.Request.Path.StartsWithSegments("/swagger", StringComparison.OrdinalIgnoreCase))
        {
            await next(context);
            return;
        }

        var originalBody = context.Response.Body;
        using var buffer = new MemoryStream();
        context.Response.Body = buffer;
        context.Response.OnStarting(() =>
        {
            // The body length changes after injection; a stale Content-Length would truncate the response.
            context.Response.ContentLength = null;
            return Task.CompletedTask;
        });

        try
        {
            await next(context);

            buffer.Position = 0;
            context.Response.Body = originalBody;

            if (context.Response.ContentType?.StartsWith("text/html", StringComparison.OrdinalIgnoreCase) == true)
            {
                using var reader = new StreamReader(buffer, Encoding.UTF8);
                var html = await reader.ReadToEndAsync(context.RequestAborted);
                context.Response.ContentLength = null;
                await context.Response.WriteAsync(InjectNonces(html, nonceProvider.Nonce), Encoding.UTF8, context.RequestAborted);
            }
            else
            {
                await buffer.CopyToAsync(originalBody, context.RequestAborted);
            }
        }
        finally
        {
            context.Response.Body = originalBody;
        }
    }

    internal static string InjectNonces(string html, string nonce)
    {
        html = ScriptTagRegex().Replace(html, match => AddNonce(match, "script", nonce));
        return StyleTagRegex().Replace(html, match => AddNonce(match, "style", nonce));
    }

    private static string AddNonce(Match match, string tagName, string nonce)
    {
        var attributes = match.Groups[1].Value;
        if (attributes.Contains("nonce=", StringComparison.OrdinalIgnoreCase))
        {
            return match.Value;
        }

        // Keep the original tag casing (e.g. <SCRIPT>) by slicing it from the match.
        var originalTagName = match.Value.Substring(1, tagName.Length);
        return $"<{originalTagName} nonce=\"{nonce}\"{attributes}>";
    }
}
