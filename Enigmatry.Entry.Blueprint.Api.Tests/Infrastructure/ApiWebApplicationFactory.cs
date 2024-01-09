using Autofac;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Autofac;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Impersonation;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Startup;
using Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;
using Enigmatry.Entry.AspNetCore.Tests.Infrastructure.TestImpersonation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure;

public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
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
        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(TestUserAuthenticationHandler.AuthenticationScheme)
                .AddScheme<TestAuthenticationOptions, TestUserAuthenticationHandler>(
                    TestUserAuthenticationHandler.AuthenticationScheme,
                    options => options.TestPrincipalFactory = () => _isUserAuthenticated ? TestUserData.CreateClaimsPrincipal() : null);
        });

        builder.ConfigureAppConfiguration((_, configBuilder) =>
        {
            configBuilder.AddConfiguration(_configuration);
        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureContainer<ContainerBuilder>(ConfigureContainer);
        return base.CreateHost(builder);
    }

    private void ConfigureContainer(ContainerBuilder builder)
    {
        builder.AppRegisterModules(_configuration);

        builder.RegisterModule<TestModule>(); // this allows certain components to be overriden

        // Api does not depend on migrations assembly, test are
        builder.RegisterModule(new EntityFrameworkModule { RegisterMigrationsAssembly = true });
    }
}
