using Autofac;
using AutoMapper;
using JetBrains.Annotations;
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
        private readonly Startup _startup;

        public TestStartup(IConfiguration configuration, IHostingEnvironment environment, ILoggerFactory loggerFactory)
        {
            _startup = new Startup(configuration, environment, loggerFactory);
        }

        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            _startup.ConfigureServicesExceptMvc(services);
            services.AddMvc(options => { options.DefaultConfigure(); })
                .AddApplicationPart(typeof(Startup).Assembly);
        }

        [UsedImplicitly]
        public void ConfigureContainer(ContainerBuilder builder)
        {
            _startup.ConfigureContainer(builder);
            builder.RegisterModule<TestModule>();
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime)
        {
            _startup.Configure(app, env);
            applicationLifetime.ApplicationStopped.Register(Mapper.Reset);
        }
    }
}