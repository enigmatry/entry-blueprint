using Autofac;
using Autofac.Extensions.DependencyInjection;
using Enigmatry.Blueprint.Infrastructure.Api.Init;
using JetBrains.Annotations;
using Serilog;

namespace Enigmatry.Blueprint.Api
{
    [UsedImplicitly]
    public static class Program
    {
        public static void Main(string[] args)
        {
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

                builder.AppAddAzureKeyVault();

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
        }
    }
}
