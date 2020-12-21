using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules
{
    public class ServiceModule : Module
    {
        public IEnumerable<Assembly> Assemblies { get; set; } = Array.Empty<Assembly>();

        protected override void Load(ContainerBuilder builder) =>
            builder.RegisterAssemblyTypes(Assemblies.ToArray())
                .Where(
                    type => type.Name.EndsWith("Service", StringComparison.InvariantCulture) || type.Name.EndsWith("Provider", StringComparison.InvariantCulture)
                ).AsImplementedInterfaces().InstancePerLifetimeScope();
    }
}
