using Enigmatry.Blueprint.Api.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema.Generation;

namespace Enigmatry.Blueprint.Api.Infrastructure.Init
{
    public static class SwaggerStartupExtensions
    {
        public static void AppUseSwagger(this IApplicationBuilder app)
        {
            // Middleware to serve SwaggerUI
            // and JSON endpoint (/swagger/v1/swagger.json)
            // https://github.com/RicoSuter/NSwag/wiki/AspNetCore-Middleware

            app.UseOpenApi();
            app.UseSwaggerUi3(c => c.Path = "");
        }

        public static void AppAddSwagger(this IServiceCollection services, string appTitle, string appVersion = "v1")
        {
            services.AddOpenApiDocument(settings =>
            {
                settings.DocumentName = appVersion;
                settings.Title = appTitle;
                settings.Version = appVersion;
                settings.SchemaNameGenerator = new DefaultSchemaNameGenerator();
                settings.SchemaNameGenerator = new CustomSwaggerSchemaNameGenerator();
            });
        }
    }
}
