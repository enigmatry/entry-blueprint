using System;
using Autofac;
using Enigmatry.Blueprint.Scheduler.Attributes;
using Enigmatry.Blueprint.Scheduler.Extensions;
using Enigmatry.Blueprint.Scheduler.Jobs.Common;

namespace Enigmatry.Blueprint.Scheduler.Autofac.Modules
{
    public class JobsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.RegisterAssemblyTypes(typeof(JobBase).Assembly)
                .Where(
                    type =>
                        type.ImplementsInterface(typeof(IJobBase))
                        && type.HasValidHandlerAttributes<JobNameAttribute>()
                )
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ServiceHost>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
