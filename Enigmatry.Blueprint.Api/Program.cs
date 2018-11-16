using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            // The ConfigureServices call here allows for
            // ConfigureContainer to be supported in Startup with
            // a strongly-typed ContainerBuilder
            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel((context, options) => { options.AddServerHeader = false; })
                .UseApplicationInsights()
                .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>()
                .UseSerilog();
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