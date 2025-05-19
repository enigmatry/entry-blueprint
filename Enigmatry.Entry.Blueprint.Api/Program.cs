using Enigmatry.Entry.Blueprint.Api;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.Blueprint.Infrastructure.Configuration;
using Enigmatry.Entry.Blueprint.Infrastructure.Init;
using Enigmatry.Entry.ServiceDefaults;
using Serilog;

internal class Program
{
    public static void Main(string[] args)
    {
        var bootstrapConfiguration = ConfigurationHelper.CreateBoostrapConfiguration();
        Log.Logger = new LoggerConfiguration()
            .AppConfigureSerilog(bootstrapConfiguration)
            .CreateBootstrapLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddTestConfiguration();

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.AddServerHeader = false;
            });

            builder.Configuration.AppAddAzureKeyVault(builder.Configuration);
            builder.Services.AppAddServices(builder.Configuration, builder.Environment);
            builder.Host.AppConfigureHost(builder.Configuration);
            builder.AddServiceDefaults();

            var app = builder.Build();

            app.MapDefaultEndpoints();
            app.AppConfigureWebApplication();
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
    }
}
