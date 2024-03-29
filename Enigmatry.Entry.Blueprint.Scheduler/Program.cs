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
            .ConfigureServices((context, services) =>
            {
                services.AppAddMediatR(AssemblyFinder.SchedulerAssembly);

                using var factory = new SerilogLoggerFactory();
                services.AddEntryQuartz(context.Configuration, AssemblyFinder.SchedulerAssembly, factory.CreateLogger<Program>(),
                    quartz => quartz.AddEntryApplicationInsights());
                services.AddApplicationInsightsTelemetryWorkerService();
            })
            .ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.AppRegisterModules();
                containerBuilder.AppRegisterCurrentUserProvider<SystemUserProvider>();
            });
}
