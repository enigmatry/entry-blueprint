#pragma warning disable CA1506
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;
using Enigmatry.Entry.Blueprint.Scheduler;
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
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {

        containerBuilder.RegisterAssemblyModules(AssemblyFinder.InfrastructureAssembly);

        containerBuilder.RegisterModule(new ServiceModule
        {
            Assemblies = new[]
            {
                AssemblyFinder.Find("Enigmatry.Entry.Infrastructure"),
                AssemblyFinder.ApplicationServicesAssembly,
                AssemblyFinder.InfrastructureAssembly
            }
        });

        containerBuilder.RegisterType<SchedulerUserProvider>()
            .As<ICurrentUserProvider>().InstancePerLifetimeScope();
    })
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
#pragma warning restore CA1506
