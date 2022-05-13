﻿using System.Reflection;
using Enigmatry.BuildingBlocks.Core.Helpers;
using Enigmatry.Blueprint.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights.TelemetryConverters;
using Enigmatry.BuildingBlocks.AspNetCore.ApplicationInsights;

namespace Enigmatry.Blueprint.Infrastructure.Api.Init;

public static class SerilogProgramHelper
{
    private static IConfiguration Configuration { get; } =
        new ConfigurationBuilder() // needed because of Serilog file configuration.
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                true)
            .Build();

    public static void AppConfigureSerilog()
    {
        LoggerConfiguration config = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithProcessId()
            .Enrich.WithMachineName()
            .Enrich.With(new OperationIdEnricher())
            .Enrich.WithProperty("AppVersion", Assembly.GetEntryAssembly()!.GetName().Version!);

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
