using System.Security.Claims;
using System.Security.Principal;
using Autofac;
using AutoMapper;
using Enigmatry.Blueprint.Api.Logging;
using Enigmatry.Blueprint.Infrastructure.Autofac.Modules;
using Enigmatry.Blueprint.Infrastructure.Data.Conventions;
using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;
using Enigmatry.Blueprint.Infrastructure.Identity;
using JetBrains.Annotations;
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
        
        public Startup(IConfiguration configuration,
            IHostingEnvironment environment,
            ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
        }

        [UsedImplicitly]
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureServicesExceptMvc(services);
            services.AddMvc(options => options.DefaultConfigure())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        internal static void ConfigureServicesExceptMvc(IServiceCollection services)
        {
            services.AddCors();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCors();
            services.AddDbContext<BlueprintContext>();
            services.AddAutoMapper();
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
            builder.RegisterModule(new ServiceModule {Assemblies = new[] {typeof(UserService).Assembly}});
            builder.RegisterModule(new EntityFrameworkModule {DbContextOptions = CreateDbContextOptions()});
            builder.RegisterModule<IdentityModule>();
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
            if (env.IsDevelopment())
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

            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("BlueprintContext"),
                b => b.MigrationsAssembly("Enigmatry.Blueprint.Data.Migrations"));

            //replace default convention builder with our so we can add custom conventions
            optionsBuilder.ReplaceService<IConventionSetBuilder, CustomSqlServerConventionSetBuilder>();

            return optionsBuilder.Options;
        }
    }
}