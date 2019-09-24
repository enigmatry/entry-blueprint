using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using Enigmatry.Blueprint.Core.Helpers;
using Enigmatry.Blueprint.Infrastructure.ApplicationInsights;
using Enigmatry.Blueprint.Infrastructure.Configuration;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights.TelemetryConverters;
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

        private static void ConfigureSerilog()
        {
            LoggerConfiguration config = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .Enrich.WithMachineName()
                .Enrich.With(new OperationIdEnricher())
                .Enrich.WithProperty("AppVersion", PlatformServices.Default.Application.ApplicationVersion)
                .WriteTo.Console(theme: SystemConsoleTheme
                    .Literate); // https://github.com/serilog/serilog-sinks-console

            AddAppInsightsToSerilog(config);

            Log.Logger = config.CreateLogger();

            // for enabling self diagnostics see https://github.com/serilog/serilog/wiki/Debugging-and-Diagnostics
            // Serilog.Debugging.SelfLog.Enable(Console.Error);
        }

        private static void AddAppInsightsToSerilog(LoggerConfiguration config)
        {
            var settings = Configuration.ReadApplicationInsightsSettings();
            if (settings.InstrumentationKey.HasContent())
            {
                config.WriteTo.ApplicationInsights(settings.InstrumentationKey, new TraceTelemetryConverter(), settings.SerilogLogsRestrictedToMinimumLevel);
            }
        }
    }
}
