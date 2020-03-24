using System;
using Autofac;
using Enigmatry.Blueprint.Scheduler.Extensions;
using Microsoft.Extensions.Configuration;
using ConfigurationExtensions = Enigmatry.Blueprint.Scheduler.Extensions.ConfigurationExtensions;

namespace Enigmatry.Blueprint.Scheduler.Autofac.Modules
{
    internal class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.Register(c => ConfigurationExtensions.Configuration).As<IConfiguration>().SingleInstance();

            builder.Register(c => c.Resolve<IConfiguration>().ReadAppSettings()).AsSelf().SingleInstance();
        }
    }
}
