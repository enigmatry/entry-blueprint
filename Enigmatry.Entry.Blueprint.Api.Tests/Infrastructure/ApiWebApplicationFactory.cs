using System.Globalization;
using Autofac;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Autofac;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Impersonation;
using Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;
using Enigmatry.Entry.Blueprint.Infrastructure.Configuration;
using Enigmatry.Entry.Blueprint.Tests.Infrastructure.Impersonation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure;

internal class ApiWebApplicationFactory(IConfiguration configuration, bool isUserAuthenticated = true) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(TestUserAuthenticationHandler.AuthenticationScheme)
                .AddScheme<TestAuthenticationOptions, TestUserAuthenticationHandler>(
                    TestUserAuthenticationHandler.AuthenticationScheme,
                    options => options.TestPrincipalFactory = () => isUserAuthenticated ? TestUserData.CreateClaimsPrincipal() : null);
        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        // https://github.com/dotnet/aspnetcore/issues/37680
        builder.ConfigureHostConfiguration(configBuilder =>
        {
            TestConfiguration.Create(b =>
            {
                b.Sources.Clear();
                b.AddConfiguration(configuration);
            });
        });
        builder.ConfigureContainer<ContainerBuilder>(ConfigureContainer);
        builder.UseSerilog((context, services, loggerConfiguration) =>
        {
            // this allows serilog log statements to appear in the test console runner
            // e.g. database sql queries (issued by DbContext) can be observed this way in the test
            loggerConfiguration.WriteTo.Console(
                restrictedToMinimumLevel: LogEventLevel.Warning,
                formatProvider: CultureInfo.InvariantCulture,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}");
        });
        return base.CreateHost(builder);
    }

    private static void ConfigureContainer(ContainerBuilder builder)
    {
        // in the api tests we need to replace current user with TestUser
        builder.RegisterModule(new TestModule(true)); // this allows certain components to be overriden

        // Api does not depend on migrations assembly, tests are
        builder.RegisterModule(new EntityFrameworkModule { RegisterMigrationsAssembly = true });
    }
}
