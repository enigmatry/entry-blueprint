using Autofac;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Autofac;
using Enigmatry.Blueprint.Infrastructure.Autofac.Modules;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure
{
    [UsedImplicitly]
    public class TestStartup
    {
        private readonly IConfiguration _configuration;
        private readonly Startup _startup;

        public TestStartup(IConfiguration configuration)
        {
            _configuration = configuration;
            _startup = new Startup(configuration);
        }

        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            Startup.AddMvc(services, _configuration)
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _startup.Configure(app, env);
        }
    }
}
