﻿using System.Reflection;
using Autofac;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Logging;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Security;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Startup;
using Enigmatry.Entry.Blueprint.Infrastructure.Configuration;
using Enigmatry.Entry.Blueprint.Infrastructure.Data;
using Enigmatry.Entry.AspNetCore.Authorization;
using Enigmatry.Entry.AspNetCore.Exceptions;
using Enigmatry.Entry.HealthChecks.Extensions;
using Microsoft.IdentityModel.Logging;
using Autofac.Extensions.DependencyInjection;
using Enigmatry.Entry.Blueprint.Infrastructure.Identity;
using Serilog;

namespace Enigmatry.Entry.Blueprint.Api;

public static class ProgramExtensions
{
    public static void AppAddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors();
        services.AddHttpContextAccessor();
        services.AddApplicationInsightsTelemetry();

        services.AppAddSettings(configuration);
        services.AppAddPolly();
        services.AppAddAutoMapper();
        services.AddEntryHealthChecks(configuration)
            .AddDbContextCheck<AppDbContext>();
        services.AppAddMediatR(AssemblyFinder.ApiAssembly);
        services.AppAddFluentValidation();

        services.AppAddAuthentication(configuration);
        services.AddEntryAuthorization<PermissionId>();

        services.AppAddSwaggerWithAzureAdAuth(configuration, "Enigmatry Blueprint Api");
        services.AppAddMvc();
    }

    public static void AppConfigureHost(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        hostBuilder.UseSerilog((context, services, loggerConfiguration) =>
        {
            loggerConfiguration
                .AppConfigureSerilog(configuration)
                .AddAppInsightsToSerilog(configuration, services);
        });
        hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        hostBuilder.ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
        {
            containerBuilder.AppRegisterModules();
            
            containerBuilder.AppRegisterClaimsPrincipalProvider();
            containerBuilder.AppRegisterCurrentUserProvider<CurrentUserProvider>();
        });
    }

    public static void AppConfigureWebApplication(this WebApplication app)
    {
        var configuration = app.Configuration;
        var env = app.Environment;

        app.UseDefaultFiles();
        app.UseStaticFiles();

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

        app.UseHttpsRedirection();
        app.UseHsts();

        app.UseEntryExceptionHandler();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<LogContextMiddleware>();

        app.MapControllers().RequireAuthorization();
        app.MapEntryHealthCheck(configuration);

        if (env.IsDevelopment())
        {
            app.AppUseSwaggerWithAzureAdAuth(configuration);
        }
        app.AppConfigureFluentValidation();
    }
}
