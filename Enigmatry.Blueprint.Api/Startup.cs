using Autofac;
using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Swagger;
using Enigmatry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Blueprint.Infrastructure.Api.Logging;
using Enigmatry.Blueprint.Infrastructure.Api.Security;
using Enigmatry.Blueprint.Infrastructure.Api.Startup;
using Enigmatry.Blueprint.Infrastructure.Configuration;
using Enigmatry.Blueprint.Infrastructure.Data;
using Enigmatry.Entry.AspNetCore.Authorization;
using Enigmatry.Entry.AspNetCore.Exceptions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Enigmatry.Entry.HealthChecks.Extensions;

namespace Enigmatry.Blueprint.Api;

[UsedImplicitly]
public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [UsedImplicitly]
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        if (_configuration.AppUseDeveloperExceptionPage())
        {
            app.UseDeveloperExceptionPage();
        }

        if (env.IsDevelopment())
        {
            app.UseCors(builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }

        app.UseHttpsRedirection();
        app.UseHsts();

        app.AppUseExceptionHandler();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<LogContextMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers().RequireAuthorization();
            endpoints.AppMapHealthCheck(_configuration);
        });

        app.AppUseSwagger();
        app.AppConfigureFluentValidation();
    }

    [UsedImplicitly]

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        ConfigureServicesExceptMvc(services, _configuration);
        services.AppAddMvc();
    }

    // this also called by tests. Mvc is configured slightly differently in integration tests
    public static void ConfigureServicesExceptMvc(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors();
        services.AddHttpContextAccessor();
        services.AddApplicationInsightsTelemetry();

        services.AppAddSettings(configuration);
        services.AppAddPolly();
        services.AppAddAutoMapper();
        services.AppAddHealthChecks(configuration)
            .AddDbContextCheck<BlueprintContext>();
        services.AppAddMediatR();

        services.AppAddAuthentication(configuration);
        services.AppAddAuthorization<PermissionId>();

        services.AppAddSwagger("Blueprint API");

        // must be PostConfigure due to: https://github.com/aspnet/Mvc/issues/7858
        services.PostConfigure<ApiBehaviorOptions>(options => options.AppAddFluentValidationApiBehaviorOptions());
    }

    [UsedImplicitly]

    // ConfigureContainer is where you can register things directly
    // with Autofac. This runs after ConfigureServices so the things
    // here will override registrations made in ConfigureServices.
    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.AppRegisterModules(_configuration);
    }
}
