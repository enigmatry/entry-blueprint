using Autofac;
using AutoMapper;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Autofac;
using Enigmatry.Blueprint.Infrastructure.Data.Conventions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure
{
    [UsedImplicitly]
    public class TestStartup
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;
        private readonly Startup _startup;

        public TestStartup(IConfiguration configuration, IHostingEnvironment environment, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
            _startup = new Startup(configuration, environment, loggerFactory);
        }

        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            Startup.ConfigureServicesExceptMvc(services);
            Startup.AddMvc(services, _configuration, _loggerFactory)
                .AddApplicationPart(typeof(Startup).Assembly);
        }

        [UsedImplicitly]
        public void ConfigureContainer(ContainerBuilder builder)
        {
          DbContextOptions dbContextOptions = CreateDbContextOptions();
            _startup.ConfigureContainerInternal(builder, dbContextOptions);
            builder.RegisterModule<TestModule>();// this allows certain components to be overriden
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime)
        {
            _startup.Configure(app, env);
            applicationLifetime.ApplicationStopped.Register(Mapper.Reset);
        }

        private DbContextOptions CreateDbContextOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("BlueprintContext"), 
                b => b.MigrationsAssembly("Enigmatry.Blueprint.Data.Migrations"));

            //replace default convention builder with our so we can add custom conventions
            optionsBuilder.ReplaceService<IConventionSetBuilder, CustomSqlServerConventionSetBuilder>();

            return optionsBuilder.Options;
        }
    }
}