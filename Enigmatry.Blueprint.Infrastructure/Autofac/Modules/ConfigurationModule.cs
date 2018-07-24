using System;
using Autofac;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Core.Settings;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ReadAppSettings)
                .AsSelf()
                .SingleInstance();

            builder.Register(c => c.Resolve<AppSettings>().InnerSettings).As<InnerSettings>()
                .SingleInstance();
        }

        private static AppSettings ReadAppSettings(IComponentContext c)
        {
            var appSettings = c.Resolve<IConfiguration>().GetSection("App").Get<AppSettings>();
            if (appSettings == null)
            {
                throw new InvalidOperationException("App section is missing from configuration.");
            }

            return appSettings;
        }
    }
}