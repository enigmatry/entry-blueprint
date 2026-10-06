using System.Text;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Csp;
using Microsoft.AspNetCore.Http;
using Shouldly;

namespace Enigmatry.Entry.Blueprint.Api.Tests.CoreFeatures.Csp;

[Category("unit")]
public class SwaggerNonceMiddlewareFixture
{
    private const string Nonce = "test-nonce";

    [Test]
    public void GivenInlineScript_NonceIsAdded()
    {
        var html = "<script>window.x = 1;</script>";

        var result = SwaggerNonceMiddleware.InjectNonces(html, Nonce);

        result.ShouldBe($"<script nonce=\"{Nonce}\">window.x = 1;</script>");
    }

    [Test]
    public void GivenExternalScript_NonceIsAddedAndAttributesAreKept()
    {
        var html = "<script src=\"swagger-ui-bundle.js\" charset=\"utf-8\"></script>";

        var result = SwaggerNonceMiddleware.InjectNonces(html, Nonce);

        result.ShouldBe($"<script nonce=\"{Nonce}\" src=\"swagger-ui-bundle.js\" charset=\"utf-8\"></script>");
    }

    [Test]
    public void GivenStyleTag_NonceIsAdded()
    {
        var html = "<style>body { margin: 0; }</style>";

        var result = SwaggerNonceMiddleware.InjectNonces(html, Nonce);

        result.ShouldBe($"<style nonce=\"{Nonce}\">body {{ margin: 0; }}</style>");
    }

    [Test]
    public void GivenTagThatAlreadyHasNonce_TagIsUnchanged()
    {
        var html = "<script nonce=\"existing\">x</script><style nonce=\"existing\">y</style>";

        var result = SwaggerNonceMiddleware.InjectNonces(html, Nonce);

        result.ShouldBe(html);
    }

    [Test]
    public void GivenUpperCaseTags_NonceIsAdded()
    {
        var html = "<SCRIPT>x</SCRIPT>";

        var result = SwaggerNonceMiddleware.InjectNonces(html, Nonce);

        result.ShouldBe($"<SCRIPT nonce=\"{Nonce}\">x</SCRIPT>");
    }

    [Test]
    public async Task GivenSwaggerHtmlResponse_BodyIsRewrittenAndContentLengthIsCleared()
    {
        var context = CreateContext("/swagger/index.html");
        var middleware = new SwaggerNonceMiddleware(async ctx =>
        {
            ctx.Response.ContentType = "text/html; charset=utf-8";
            ctx.Response.ContentLength = 20;
            await ctx.Response.WriteAsync("<script>x</script>");
        });
        using var nonceProvider = new NonceProvider();

        await middleware.InvokeAsync(context, nonceProvider);

        (await ReadBodyAsync(context)).ShouldBe($"<script nonce=\"{nonceProvider.Nonce}\">x</script>");
        context.Response.ContentLength.ShouldBeNull();
    }

    [Test]
    public async Task GivenJsonResponse_BodyIsUnchanged()
    {
        const string json = "{\"openapi\":\"3.0.0\"}";
        var context = CreateContext("/swagger/v1/swagger.json");
        var middleware = new SwaggerNonceMiddleware(async ctx =>
        {
            ctx.Response.ContentType = "application/json";
            await ctx.Response.WriteAsync(json);
        });
        using var nonceProvider = new NonceProvider();

        await middleware.InvokeAsync(context, nonceProvider);

        (await ReadBodyAsync(context)).ShouldBe(json);
    }

    [Test]
    public async Task GivenNonSwaggerPath_ResponseIsNotBuffered()
    {
        const string body = "<script>x</script>";
        var context = CreateContext("/api/users");
        var middleware = new SwaggerNonceMiddleware(async ctx =>
        {
            ctx.Response.ContentType = "text/html";
            await ctx.Response.WriteAsync(body);
        });
        using var nonceProvider = new NonceProvider();

        await middleware.InvokeAsync(context, nonceProvider);

        (await ReadBodyAsync(context)).ShouldBe(body);
    }

    private static DefaultHttpContext CreateContext(string path)
    {
        var context = new DefaultHttpContext { Request = { Path = path } };
        context.Response.Body = new MemoryStream();
        return context;
    }

    private static async Task<string> ReadBodyAsync(HttpContext context)
    {
        context.Response.Body.Position = 0;
        using var reader = new StreamReader(context.Response.Body, Encoding.UTF8);
        return await reader.ReadToEndAsync();
    }
}
