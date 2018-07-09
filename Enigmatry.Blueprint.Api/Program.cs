using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using Enigmatry.Blueprint.Data.Migrations;
using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;
using JetBrains.Annotations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Enigmatry.Blueprint.Api
{
    [UsedImplicitly]
    public class Program
    {
        private static IConfiguration Configuration { get; } =
            new ConfigurationBuilder() // needed because of Serilog file configuration.
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                    true)
                .Build();

        private static string ApplicationInsightsInstrumentationKey =>
            Configuration["ApplicationInsights:InstrumentationKey"];

        public static void Main(string[] args)
        {
            ConfigureSerilog();
            try
            {
                Log.Information("Starting web host");
                IWebHost host = BuildWebHost(args);

                InitializeDb(host);

                host.Run();
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

        private static IWebHost BuildWebHost(string[] args)
        {
            IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>()
                .UseSerilog();

            if (!string.IsNullOrEmpty(ApplicationInsightsInstrumentationKey))
            {
                builder.UseApplicationInsights();
            }

            return builder.Build();
        }

        // TODO: Vladan found a better approach - use approach from SBS or Index Init
        private static void InitializeDb(IWebHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<BlueprintContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }

        private static void ConfigureSerilog()
        {
            LoggerConfiguration config = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("AppVersion", PlatformServices.Default.Application.ApplicationVersion)
                .WriteTo.Console(theme: SystemConsoleTheme
                    .Literate); // https://github.com/serilog/serilog-sinks-console

            if (!string.IsNullOrEmpty(ApplicationInsightsInstrumentationKey))
            {
                config.WriteTo.ApplicationInsightsTraces(ApplicationInsightsInstrumentationKey,
                    LogEventLevel.Information);
            }

            Log.Logger = config.CreateLogger();
            // for enabling self diagnostics see https://github.com/serilog/serilog/wiki/Debugging-and-Diagnostics
            // Serilog.Debugging.SelfLog.Enable(Console.Error);
        }
    }
}