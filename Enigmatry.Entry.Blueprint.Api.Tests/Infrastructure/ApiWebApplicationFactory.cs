using Autofac;
using Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;
using Enigmatry.Entry.Blueprint.Infrastructure.Configuration;
using Enigmatry.Entry.Blueprint.Infrastructure.Tests;
using Enigmatry.Entry.Blueprint.Infrastructure.Tests.Autofac;
using Enigmatry.Entry.Blueprint.Infrastructure.Tests.Impersonation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure;

internal class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly IConfiguration _configuration;
    private readonly bool _isUserAuthenticated;

    public ApiWebApplicationFactory(IConfiguration configuration, bool isUserAuthenticated = true)
    {
        _configuration = configuration;
        _isUserAuthenticated = isUserAuthenticated;
        
        // needed so that log statements from the Api appear in the test console runner
        Server.PreserveExecutionContext = true;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(TestUserAuthenticationHandler.AuthenticationScheme)
                .AddScheme<TestAuthenticationOptions, TestUserAuthenticationHandler>(
                    TestUserAuthenticationHandler.AuthenticationScheme,
                    options => options.TestPrincipalFactory = () => _isUserAuthenticated ? TestUserData.CreateClaimsPrincipal() : null);
        });

    protected override IHost CreateHost(IHostBuilder builder)
    {
        // https://github.com/dotnet/aspnetcore/issues/37680
        builder.ConfigureHostConfiguration(_ =>
        {
            TestConfiguration.Create(b =>
            {
                b.Sources.Clear();
                b.AddConfiguration(_configuration);
            });
        });
        builder.ConfigureContainer<ContainerBuilder>(ConfigureContainer);
        builder.UseSerilog((_, _, loggerConfiguration) =>
        {
            loggerConfiguration.ConfigureSerilogForIntegrationTests();
        });
        return base.CreateHost(builder);
    }

    private static void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule<IdentityModule<TestUserProvider>>();
        builder.RegisterModule<TestModule>(); // this allows certain components to be overriden
    }
}
