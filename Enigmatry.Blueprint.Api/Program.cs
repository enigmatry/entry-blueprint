using Autofac;
using Autofac.Extensions.DependencyInjection;
using Enigmatry.Blueprint.Api;
using Enigmatry.Blueprint.Infrastructure.Api.Init;
using Serilog;

SerilogProgramHelper.AppConfigureSerilog();
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.WebHost.ConfigureKestrel(options =>
    {
        options.AddServerHeader = false;
    });

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
}
finally
{
    Log.Information("Stopping web host");
    Log.CloseAndFlush();
}
