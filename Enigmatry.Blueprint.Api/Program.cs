using Autofac;
using Autofac.Extensions.DependencyInjection;
using Enigmatry.Blueprint.Api;
using Enigmatry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Blueprint.Infrastructure.Init;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.AppConfigureSerilog();

    builder.WebHost.ConfigureKestrel(options =>
    {
        options.AddServerHeader = false;
    });

    var configuration = ConfigurationHelper.CreateConfiguration();
    builder.Configuration.AppAddAzureKeyVault(configuration);

    var startup = new Startup(builder.Configuration);
    startup.ConfigureServices(builder.Services);


    builder.Host.UseSerilog();
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(startup.ConfigureContainer);

    var app = builder.Build();

    startup.Configure(app, app.Environment);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    throw;
}
finally
{
    Log.Information("Stopping web host");
    Log.CloseAndFlush();
}
