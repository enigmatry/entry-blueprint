using Autofac;
using AutoMapper;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Autofac;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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