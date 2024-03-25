using Enigmatry.Entry.Scheduler;
using Serilog;
using Serilog.Extensions.Logging;
using System.Reflection;

var builder = Host.CreateDefaultBuilder(args);

builder.UseSerilog((context, services, configuration) =>
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services));

builder.ConfigureServices((context, services) =>
{
    using var factory = new SerilogLoggerFactory();
    services.AddEntryQuartz(context.Configuration, Assembly.GetExecutingAssembly(), factory.CreateLogger<Program>(),
        quartz => quartz.AddEntryApplicationInsights());
    services.AddApplicationInsightsTelemetryWorkerService();
});

var host = builder.Build();

host.Run();
