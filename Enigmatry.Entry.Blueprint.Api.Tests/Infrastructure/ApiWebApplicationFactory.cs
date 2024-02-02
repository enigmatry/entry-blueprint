using System.Globalization;
using Autofac;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Autofac;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Impersonation;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Startup;
using Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure;

internal class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly IConfiguration _configuration;
    private readonly bool _isUserAuthenticated;

    public ApiWebApplicationFactory(IConfiguration configuration, bool isUserAuthenticated = true)
    {
        _configuration = configuration;
        _isUserAuthenticated = isUserAuthenticated;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // this setting is needed to pass information to the Program.cs args.
        // There is no other way, since ConfigureAppConfiguration is called later
        builder.UseSetting("IsTest", "true");
        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(TestUserAuthenticationHandler.AuthenticationScheme)
                .AddScheme<TestAuthenticationOptions, TestUserAuthenticationHandler>(
                    TestUserAuthenticationHandler.AuthenticationScheme,
                    options => options.TestPrincipalFactory = () => _isUserAuthenticated ? TestUserData.CreateClaimsPrincipal() : null);
        });

        builder.ConfigureAppConfiguration((_, configBuilder) =>
        {
            // if the appSettings.json and appSettings.Development.json are messing too much with tests
            // un-comment out the next line.
            // configBuilder.Sources.Clear();
            configBuilder.AddConfiguration(_configuration);
        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureContainer<ContainerBuilder>(ConfigureContainer);
        builder.UseSerilog((context, services, configuration) =>
        {
            // this allows serilog log statements to appear in the test console runner
            // e.g. database sql queries (issued by DbContext) can be observed this way in the test
            configuration.WriteTo.Console(
                restrictedToMinimumLevel: LogEventLevel.Debug,
                formatProvider: CultureInfo.InvariantCulture,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}");
        });
        return base.CreateHost(builder);
    }

    private static void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule<TestModule>(); // this allows certain components to be overriden

        // Api does not depend on migrations assembly, tests are
        builder.RegisterModule(new EntityFrameworkModule { RegisterMigrationsAssembly = true });
    }
}
