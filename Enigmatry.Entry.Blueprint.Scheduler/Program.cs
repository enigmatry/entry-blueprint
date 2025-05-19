using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Startup;
using Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;
using Enigmatry.Entry.Blueprint.Infrastructure.Identity;
using Enigmatry.Entry.Blueprint.Infrastructure.Init;
using Enigmatry.Entry.Blueprint.Scheduler;
using Enigmatry.Entry.Scheduler;
using Enigmatry.Yessa.ServiceDefaults;
using Serilog;
using Serilog.Extensions.Logging;

internal class Program
{
    public static void Main() =>
        CreateHostBuilder()
            .Build()
            .Run();

    internal static IHostBuilder CreateHostBuilder() =>
        Host.CreateDefaultBuilder()
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureServices((context, services) =>
            {
                services.AppAddMediatR(AssemblyFinder.SchedulerAssembly);

                using var factory = new SerilogLoggerFactory();
                services.AddEntryQuartz(context.Configuration, AssemblyFinder.SchedulerAssembly, factory.CreateLogger<Program>(),
                    quartz => quartz.AddJobListener<OpenTelemetryJobListener>());

                services.AddOpenTelemetryWorkerService(context.Configuration);
            })
            .ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.AppRegisterModules();
                containerBuilder.RegisterModule<IdentityModule<SystemUserProvider>>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ConfigureOpenTelemetryLogging();
            })
            .UseSerilog((context, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(context.Configuration);
                    //.ReadFrom.Services(services);
            }, writeToProviders: true);
}
