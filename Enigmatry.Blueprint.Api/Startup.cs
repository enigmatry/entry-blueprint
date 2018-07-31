using System.Security.Claims;
using System.Security.Principal;
using Autofac;
using AutoMapper;
using Enigmatry.Blueprint.Api.Logging;
using Enigmatry.Blueprint.Api.Models.Identity;
using Enigmatry.Blueprint.Api.Models.Validation;
using Enigmatry.Blueprint.Infrastructure.ApplicationServices.Identity;
using Enigmatry.Blueprint.Infrastructure.Autofac.Modules;
using Enigmatry.Blueprint.Infrastructure.Data.Conventions;
using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;
using Enigmatry.Blueprint.Model.Identity;
using FluentValidation.AspNetCore;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Api
{
    [UsedImplicitly]
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IConfiguration configuration,
            IHostingEnvironment environment,
            ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        [UsedImplicitly]
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddMvc(services, _configuration, _loggerFactory);
            ConfigureServicesExceptMvc(services);
        }

        // IMvcBuilder needed for tests
        internal static IMvcBuilder AddMvc(IServiceCollection services, IConfiguration configuration,
            ILoggerFactory loggerFactory)
        {
            return services
                .AddMvc(options => options.DefaultConfigure(configuration, loggerFactory))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv =>
                {
                    // disables standard data annotations validation
                    // https://github.com/JeremySkinner/FluentValidation/wiki/i.-ASP.NET-Core-integration
                    // fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false; // 
                    fv.RegisterValidatorsFromAssemblyContaining<UserCreateOrUpdateCommandValidator>();
                });
        }

        internal static void ConfigureServicesExceptMvc(IServiceCollection services)
        {
            services.AddCors();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<BlueprintContext>();
            services.AddAutoMapper();
            services.AddMediatR(
                typeof(UserModel).Assembly, // this assembly
                typeof(UserCreatedDomainEvent).Assembly, // domain assembly
                typeof(UserCreatedDomainEventHandler).Assembly);

            // must be PostConfigure due to: https://github.com/aspnet/Mvc/issues/7858
            services.PostConfigure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var model = new ValidationErrorModel(actionContext.ModelState);
                    return new BadRequestObjectResult(model);
                };
            });
        }

        [UsedImplicitly]
        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            ConfigureContainerInternal(builder, CreateDbContextOptions());
        }

        internal void ConfigureContainerInternal(ContainerBuilder builder, DbContextOptions options)
        {
            builder.RegisterModule<ConfigurationModule>();
            builder.Register(GetPrincipal)
                .As<IPrincipal>().InstancePerLifetimeScope();
            builder.RegisterModule(new ServiceModule {Assemblies = new[] {typeof(UserService).Assembly}});
            builder.RegisterModule(new EntityFrameworkModule {DbContextOptions = options});
            builder.RegisterModule<IdentityModule>();
            builder.RegisterModule(new EventBusModule { AzureServiceBusEnabled = _configuration.AppSettings().ServiceBus.AzureServiceBusEnabled});
        }

        private static ClaimsPrincipal GetPrincipal(IComponentContext c)
        {
            var httpContextAccessor = c.Resolve<IHttpContextAccessor>();
            ClaimsPrincipal user = httpContextAccessor.HttpContext.User;
            return user;
        }

        [UsedImplicitly]
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (_configuration.UseDeveloperExceptionPage())
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

            app.UseMvc();
        }

        private DbContextOptions CreateDbContextOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            //TODO enable logging
            //optionsBuilder.UseLoggerFactory().EnableSensitiveDataLogging(false);

            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("BlueprintContext"));

            //replace default convention builder with our so we can add custom conventions
            optionsBuilder.ReplaceService<IConventionSetBuilder, CustomSqlServerConventionSetBuilder>();

            return optionsBuilder.Options;
        }
    }
}