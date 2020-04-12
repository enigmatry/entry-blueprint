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

            builder.RegisterModule(new QuartzAutofacFactoryModule { ConfigurationProvider = c => c.Resolve<IConfiguration>().ReadAsNameValueCollection("quartz") });

            builder.RegisterModule(new QuartzAutofacJobsModule(GetType().Assembly));
        }
    }
}
