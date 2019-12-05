using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Enigmatry.Blueprint.Api.Init
{
    public static class SwaggerStartupExtensions
    {
        public static void AppUseSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blueprint Api V1"); 
                c.RoutePrefix = String.Empty;
            });
        }

        public static void AppAddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(SetupSwaggerAction);
        }

        private static void SetupSwaggerAction(SwaggerGenOptions c)
        {
            c.SwaggerDoc("v1", new OpenApiInfo 
            {
                Title = "Blueprint Api",
                Version = "v1",
                Description = "Blueprint Api",
                Contact = new OpenApiContact
                {
                    Name = "TBD",
                    Email = "tbd@tbd.com",
                    Url = new Uri("https://www.enigmatry.com")
                }
            });

            // TODO: uncomment after authentication scheme is setup
            //c.OperationFilter<AuthResponsesOperationFilter>();

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            // Scan FluentValidations Rules to generate the Swagger documentation
            c.AddFluentValidationRules();
        }
    }
}
