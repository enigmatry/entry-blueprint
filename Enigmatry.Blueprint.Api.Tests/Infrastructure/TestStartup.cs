using Autofac;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Autofac;
using Enigmatry.Blueprint.Infrastructure.Autofac.Modules;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure
{
    [UsedImplicitly]
    public class TestStartup
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;
        private readonly Startup _startup;

        public TestStartup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
            _startup = new Startup(configuration, loggerFactory);
        }

        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            Startup.AddMvc(services, _configuration, _loggerFactory)
                .AddApplicationPart(typeof(Startup).Assembly);
            Startup.ConfigureServicesExceptMvc(services, _configuration);
        }

        [UsedImplicitly]
        public void ConfigureContainer(ContainerBuilder builder)
        {
            _startup.ConfigureContainer(builder);
            builder.RegisterModule<TestModule>();// this allows certain components to be overriden
            // Api does not depend on migrations assembly, test are
            builder.RegisterModule(new EntityFrameworkModule {RegisterMigrationsAssembly = true});
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            _startup.Configure(app, env);
        }
    }
}
