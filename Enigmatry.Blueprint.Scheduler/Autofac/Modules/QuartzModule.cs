using System;
using System.Collections.Specialized;
using Autofac;
using Autofac.Extras.Quartz;
using Enigmatry.Blueprint.Scheduler.Extensions;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Scheduler.Autofac.Modules
{
    public class QuartzModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            //TODO: casting quartz section return empty collection
            builder.RegisterModule(new QuartzAutofacFactoryModule {ConfigurationProvider = c => c.Resolve<IConfiguration>().ReadSettingsSection<IConfigurationSection>("quartz").Get<NameValueCollection>()});

            builder.RegisterModule(new QuartzAutofacJobsModule(GetType().Assembly));
        }
    }
}
