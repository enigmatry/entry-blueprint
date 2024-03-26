using Autofac;
using Autofac.Extensions.DependencyInjection;
using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Startup;
using Enigmatry.Entry.Blueprint.Scheduler;
using Enigmatry.Entry.Scheduler;
using Serilog;
using Serilog.Extensions.Logging;
using System.Reflection;

internal class Program
{
    public static void Main() =>
        CreateHostBuilder()
            .Build()
            .Run();

    internal static IHostBuilder CreateHostBuilder() =>
        Host.CreateDefaultBuilder()
            .UseSerilog((context, services, configuration) =>
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services))
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.AppRegisterModulesExceptIPrincipal();

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
}
