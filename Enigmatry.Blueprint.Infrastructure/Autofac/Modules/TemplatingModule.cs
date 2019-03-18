using Autofac;
using Enigmatry.Blueprint.ApplicationServices;
using Enigmatry.Blueprint.Infrastructure.Templating;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules
{
    public class TemplatingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RazorTemplatingEngine>().As<ITemplatingEngine>().InstancePerLifetimeScope();
        }
    }
}
