using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using Autofac;
using AutoMapper;
using Enigmatry.Blueprint.Api.Logging;
using Enigmatry.Blueprint.Api.Models.Identity;
using Enigmatry.Blueprint.ApplicationServices.Identity;
using Enigmatry.Blueprint.Infrastructure;
using Enigmatry.Blueprint.Infrastructure.Autofac.Modules;
using Enigmatry.Blueprint.Infrastructure.Data.Conventions;
using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;
using Enigmatry.Blueprint.Infrastructure.MediatR;
using Enigmatry.Blueprint.Infrastructure.Validation;
using Enigmatry.Blueprint.Model.Identity;
using FluentValidation;
using FluentValidation.AspNetCore;
using JetBrains.Annotations;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

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
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv =>
                {
                    // disables standard data annotations validation
                    // https://fluentvalidation.net/aspnet.html#asp-net-core
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false; 
                    fv.ImplicitlyValidateChildProperties = true;
                    fv.RegisterValidatorsFromAssemblyContaining<UserCreateOrUpdateCommandValidator>();
                });
        }

        internal static void ConfigureServicesExceptMvc(IServiceCollection services)
        {
            services.AddCors();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<BlueprintContext>();
            services.AddAutoMapper();
            
            // add MediatR
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>)); 
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); 
            services.AddScoped(typeof(IRequestPreProcessor<>), typeof(SamplePreRequestBehavior<>)); 
            services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(SamplePostRequestBehavior<,>)); 
            services.AddMediatR(
                typeof(UserModel).Assembly, // this assembly
                typeof(UserCreatedDomainEvent).Assembly, // domain assembly
                typeof(UserCreatedDomainEventHandler).Assembly);

            // must be PostConfigure due to: https://github.com/aspnet/Mvc/issues/7858
            services.PostConfigure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context => context.HttpContext.CreateValidationProblemDetailsResponse(context.ModelState);
            });

            services.AddSwaggerGen(SetupSwaggerAction);
        }

        private static void SetupSwaggerAction(SwaggerGenOptions c)
        {
            c.SwaggerDoc("v1", new Info
            {
                Title = "Blueprint Api",
                Version = "v1",
                Description = "Blueprint Api",
                Contact = new Contact
                {
                    Name = "TBD",
                    Email = "tbd@tbd.com",
                    Url = "https://www.enigmatry.com"
                }
            });

            // TODO: uncomment after authentication scheme is setup
            //c.OperationFilter<AuthResponsesOperationFilter>();

            // Set the comments path for the Swagger JSON and UI.
            string path = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
                Assembly.GetExecutingAssembly().GetName().Name + ".xml");
            c.IncludeXmlComments(path);

            // Scan FluentValidations Rules to generate the Swagger documentation
            c.AddFluentValidationRules();
        }

        [UsedImplicitly]
        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ConfigurationModule>();
            builder.Register(GetPrincipal)
                .As<IPrincipal>().InstancePerLifetimeScope();
            builder.RegisterModule(new ServiceModule {Assemblies = new[] {typeof(UserService).Assembly, typeof(TimeProvider).Assembly}});
            builder.RegisterModule<EntityFrameworkModule>();
            builder.RegisterModule<IdentityModule>();
            builder.RegisterModule(new EventBusModule
            {
                AzureServiceBusEnabled = _configuration.ReadAppSettings().ServiceBus.AzureServiceBusEnabled
            });
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
            ValidatorOptions.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;

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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blueprint Api V1"); });

            app.UseMvc();
        }
    }
}