using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Enigmatry.Blueprint.Scheduler.Configurations;
using Enigmatry.Blueprint.Scheduler.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Topshelf;
using Topshelf.Autofac;

namespace Enigmatry.Blueprint.Scheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureSerilog();
            
            try
            {
                Log.Information("Starting service host...");
                BuildServiceHost();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Service host terminated unexpectedly!");
            }
            finally
            {
                Log.Information("Stopping service host.");
                Log.CloseAndFlush();
            }
        }

        private static void BuildServiceHost()
        {
            var services = new ServiceCollection().AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(Program).Assembly);
            builder.Populate(services);
            var container = builder.Build();

            HostFactory.Run(x =>
            {
                var appSettings = container.Resolve<AppSettings>();

                x.RunAsPrompt();
                x.UseAutofacContainer(container);
                x.UseSerilog();

                x.SetServiceName(appSettings.ServiceName);
                x.SetDisplayName(appSettings.DisplayName);
                x.SetDescription(appSettings.Description);

                x.Service<ServiceHost>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsingAutofacContainer();

                    serviceConfigurator.WhenStarted(async service => await service.WhenStarted());
                    serviceConfigurator.WhenPaused(async service => await service.WhenPaused());
                    serviceConfigurator.WhenContinued(async service => await service.WhenContinued());
                    serviceConfigurator.WhenStopped(async service => await service.WhenStopped());
                });
            });
        }

        private static void ConfigureSerilog()
        {
            LoggerConfiguration config = new LoggerConfiguration()
                .ReadFrom.Configuration(ConfigurationExtensions.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .Enrich.WithMachineName()
                .Enrich.WithAssemblyName()
                .Enrich.WithAssemblyVersion();

            Log.Logger = config.CreateLogger();

            // for enabling self diagnostics see https://github.com/serilog/serilog/wiki/Debugging-and-Diagnostics
            // Serilog.Debugging.SelfLog.Enable(Console.Error);
        }
    }
}
