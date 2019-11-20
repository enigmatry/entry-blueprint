using Autofac;
using Enigmatry.Blueprint.Api.Init;
using Enigmatry.Blueprint.Api.Logging;
using Enigmatry.Blueprint.Infrastructure.Configuration;
using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Enigmatry.Blueprint.Api
{
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
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseMiddleware<LogContextMiddleware>();
            app.UseHsts();

            app.UseCultures();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                // endpoints.MapHealthChecks("/health");
            });

            app.AppUseSwagger();
            app.AppConfigureFluentValidation();
            app.AppUseHealthChecks();
        }

        [UsedImplicitly]

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureServicesExceptMvc(services, _configuration);
            services.AppAddMvc(_configuration); 
        }

        // this also called by tests. Mvc is configured slightly differently in integration tests
        public static void ConfigureServicesExceptMvc(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<BlueprintContext>();
            services.AddApplicationInsightsTelemetry();

            services.AppAddSettings(configuration);
            services.AppAddPolly();
            services.AppAddLocalization();
            services.AppAddAutoMapper();
            services.AppAddHealthChecks(configuration);
            services.AppAddMediatR();
            services.AppAddTypedHttpClients(configuration.ReadAppSettings());
            services.AppAddSwagger();

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
}
