using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules
{
    public class ServiceModule : Module
    {
        public Assembly[] Assemblies { private get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assemblies)
                .Where(
                    type => type.Name.EndsWith("Service") || type.Name.EndsWith("Provider")
                ).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
