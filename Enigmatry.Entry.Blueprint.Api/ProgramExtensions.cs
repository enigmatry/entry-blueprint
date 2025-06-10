using Autofac;
using Autofac.Extensions.DependencyInjection;
using Enigmatry.Entry.AspNetCore.Authorization;
using Enigmatry.Entry.AspNetCore.Exceptions;
using Enigmatry.Entry.AspNetCore.Security;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Logging;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Security;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Startup;
using Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;
using Enigmatry.Entry.Blueprint.Infrastructure.Configuration;
using Enigmatry.Entry.Blueprint.Infrastructure.Data;
using Enigmatry.Entry.Blueprint.Infrastructure.Identity;
using Enigmatry.Entry.Blueprint.Infrastructure.Init;
using Enigmatry.Entry.HealthChecks.Extensions;
using Enigmatry.Entry.SmartEnums.Swagger;
using Microsoft.IdentityModel.Logging;
using Serilog;

namespace Enigmatry.Entry.Blueprint.Api;

public static class ProgramExtensions
{
    public static void AppAddServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddCors();
        services.AddHttpContextAccessor();

        services.AppAddSettings(configuration);
        services.AppAddPolly();
        services.AppAddAutoMapper();
        services.AddEntryHealthChecks(configuration)
            .AddDbContextCheck<AppDbContext>();
        services.AppAddMediatR(AssemblyFinder.ApiAssembly);
        services.AppAddFluentValidation();
        services.AddEntryHttps(env);

        services.AppAddAuthentication(configuration);
        services.AddEntryAuthorization<PermissionId>();

        services.AppAddSwaggerWithAzureAdAuth(configuration, "Enigmatry Blueprint Api", "v1", configureSettings =>
        {
            configureSettings.EntryConfigureSmartEnums();
        });
        services.AppAddMvc();
    }

    public static void AppConfigureHost(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        hostBuilder.UseSerilog((context, services, loggerConfiguration) =>
        {
            loggerConfiguration.AppConfigureSerilog(configuration);
        }, writeToProviders: true);
        hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        hostBuilder.ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
        {
            containerBuilder.AppRegisterModules();
            containerBuilder.AppRegisterClaimsPrincipalProvider();
            containerBuilder.RegisterModule<IdentityModule<CurrentUserProvider>>();
        });
    }

    public static void AppConfigureWebApplication(this WebApplication app)
    {
        var configuration = app.Configuration;
        var env = app.Environment;

        app.UseDefaultFiles();
        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = context =>
            {
                if (context.File.Name != "index.html")
                {
                    context.Context.Response.Headers.Append("Cache-Control", "public, max-age: 604800");
                }
            }
        });

        app.MapFallbackToFile("index.html");

        app.UseRouting();

        if (configuration.AppUseDeveloperExceptionPage())
        {
            app.UseDeveloperExceptionPage();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseCors(builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader());
            IdentityModelEventSource.ShowPII = true;
        }
        
        app.UseEntryHttps(app.Environment);
        app.UseEntryExceptionHandler();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<LogContextMiddleware>();
        // We add the Exception middleware twice; first time for exceptions during auth setup; second time to enrich Exception logs with the LogContext
        app.UseEntryExceptionHandler();
        
        app.MapControllers().RequireAuthorization();
        app.MapEntryHealthCheck(configuration);

        if (env.IsDevelopment())
        {
            app.AppUseSwaggerWithAzureAdAuth(configuration);
        }
        app.AppConfigureFluentValidation();
    }
}
