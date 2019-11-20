using System;
using Autofac.Extensions.DependencyInjection;
using Enigmatry.Blueprint.Api.Init;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Enigmatry.Blueprint.Api
{
    [UsedImplicitly]
    public class Program
    {
        public static void Main(string[] args)
        {
            SerilogProgramHelper.AppConfigureSerilog();
            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
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

        private static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                        {
                            options.AddServerHeader = false;
                        })
                        .UseStartup<Startup>()
                        .UseSerilog();
                });
        }
    }
}
