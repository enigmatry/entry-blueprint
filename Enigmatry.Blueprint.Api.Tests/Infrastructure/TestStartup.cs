using Autofac;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Autofac;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Impersonation;
using Enigmatry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Blueprint.Infrastructure.Autofac.Modules;
using Enigmatry.Entry.AspNetCore.Tests.Infrastructure.TestImpersonation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure;

[UsedImplicitly]
public class TestStartup
{
    private readonly Startup _startup;
    private readonly IConfiguration _configuration;
    private readonly bool _isUserAuthenticated;

    public TestStartup(IConfiguration configuration, bool isUserAuthenticated = true)
    {
        _configuration = configuration;
        _isUserAuthenticated = isUserAuthenticated;
        _startup = new Startup(configuration);
    }

    [UsedImplicitly]
    public void ConfigureServices(IServiceCollection services)
    {
        services.AppAddMvc()
            .AddApplicationPart(AssemblyFinder.ApiAssembly); // needed only because of tests

        Startup.ConfigureServicesExceptMvc(services, _configuration);

        services.AddAuthentication(TestUserAuthenticationHandler.AuthenticationScheme)
            .AddScheme<TestAuthenticationOptions, TestUserAuthenticationHandler>(
                TestUserAuthenticationHandler.AuthenticationScheme,
                options => options.TestPrincipalFactory = () => _isUserAuthenticated ? TestUserData.CreateClaimsPrincipal() : null);
    }

    [UsedImplicitly]
    public void ConfigureContainer(ContainerBuilder builder)
    {
        _startup.ConfigureContainer(builder);

        builder.RegisterModule<TestModule>(); // this allows certain components to be overriden

        // Api does not depend on migrations assembly, test are
        builder.RegisterModule(new EntityFrameworkModule { RegisterMigrationsAssembly = true });
    }

    [UsedImplicitly]
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) => _startup.Configure(app, env);
}
