using Autofac;
using Autofac.Extensions.DependencyInjection;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Startup;
using Enigmatry.Entry.Scheduler;
using Serilog;
using Serilog.Extensions.Logging;
using System.Reflection;

var builder = Host.CreateDefaultBuilder(args);

builder.UseSerilog((context, services, configuration) =>
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services));

builder
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.AppRegisterModulesExceptIPrincipal())
    .ConfigureServices((context, services) =>
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyFinder.DomainAssembly));
        using var factory = new SerilogLoggerFactory();
        services.AddEntryQuartz(context.Configuration, Assembly.GetExecutingAssembly(), factory.CreateLogger<Program>(),
            quartz => quartz.AddEntryApplicationInsights());
        services.AddApplicationInsightsTelemetryWorkerService();
    });

var host = builder.Build();

host.Run();
