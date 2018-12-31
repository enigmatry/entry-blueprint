using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using Autofac;
using AutoMapper;
using Enigmatry.Blueprint.Api.GitHubApi;
using Enigmatry.Blueprint.Api.Logging;
using Enigmatry.Blueprint.ApplicationServices.Identity;
using Enigmatry.Blueprint.Core.Settings;
using Enigmatry.Blueprint.Infrastructure;
using Enigmatry.Blueprint.Infrastructure.Autofac.Modules;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Polly;
using Polly.Registry;
using Polly.Timeout;
using Refit;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using UserModel = Enigmatry.Blueprint.Api.Models.Identity.UserModel;

namespace Enigmatry.Blueprint.Api
{
    [UsedImplicitly]
    public class Startup
    {
        private const string GlobalTimeoutPolicyName = "global-timeout";

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
            ConfigureServicesExceptMvc(services, _configuration);
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

        // this also called by tests. Mvc is configured slightly differently in integration tests
        internal static void ConfigureServicesExceptMvc(IServiceCollection services, IConfiguration configuration)
        {
            ConfigurePolly(services);

            services.AddCors();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<BlueprintContext>();
            services.AddAutoMapper();

            ConfigureConfiguration(services, configuration);
            ConfigureMediatR(services);
            ConfigureTypedClients(services, configuration);

            // must be PostConfigure due to: https://github.com/aspnet/Mvc/issues/7858
            services.PostConfigure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                    context.HttpContext.CreateValidationProblemDetailsResponse(context.ModelState);
            });

            services.AddSwaggerGen(SetupSwaggerAction);
        }

        private static void ConfigurePolly(IServiceCollection services)
        {
            // Add registry
            IPolicyRegistry<string> policyRegistry = services.AddPolicyRegistry();

            // Centrally stored policies
            TimeoutPolicy<HttpResponseMessage> timeoutPolicy =
                Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMilliseconds(1500));
            policyRegistry.Add(GlobalTimeoutPolicyName, timeoutPolicy);
        }

        private static void ConfigureConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            // Options for particular external services
            services.Configure<GitHubApiSettings>(configuration.GetSection("App:GitHubApi"));
        }

        private static void ConfigureMediatR(IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped(typeof(IRequestPreProcessor<>), typeof(SamplePreRequestBehavior<>));
            services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(SamplePostRequestBehavior<,>));
            services.AddMediatR(
                typeof(UserModel).Assembly, // this assembly
                typeof(UserCreatedDomainEvent).Assembly, // domain assembly
                typeof(UserCreatedDomainEventHandler).Assembly);
        }

        private static void ConfigureTypedClients(IServiceCollection services, IConfiguration configuration)
        {
            string baseUrl = configuration.ReadAppSettings().GitHubApi.BaseUrl;
            services.AddHttpClient("GitHub", options =>
                {
                    options.BaseAddress = new Uri(baseUrl);
                    options.Timeout = TimeSpan.FromMilliseconds(15000);
                    options.DefaultRequestHeaders.Add("User-Agent", "request");// needed to call GitHub API
                })
                // these are some examples of policies, not all are needed (e.g. both timeout policies)
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMilliseconds(1500)))
                .AddPolicyHandlerFromRegistry(GlobalTimeoutPolicyName)
                // Handle 5xx status code and any responses with a 408 (Request Timeout) status code,
                // see: https://github.com/App-vNext/Polly/wiki/Polly-and-HttpClientFactory#using-addtransienthttperrorpolicy
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddTypedClient(RestService.For<IGitHubClient>);
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
            builder.RegisterModule(new ServiceModule
                {Assemblies = new[] {typeof(UserService).Assembly, typeof(TimeProvider).Assembly}});
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