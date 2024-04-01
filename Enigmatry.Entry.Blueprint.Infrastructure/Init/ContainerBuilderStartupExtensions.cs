using Autofac;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Init;

public static class ContainerBuilderStartupExtensions
{
    public static void AppRegisterModules(this ContainerBuilder builder)
    {
        builder.RegisterAssemblyModules(AssemblyFinder.InfrastructureAssembly);

        builder.RegisterModule(new ServiceModule
        {
            Assemblies = new[]
            {
                AssemblyFinder.Find("Enigmatry.Entry.Infrastructure"),
                AssemblyFinder.ApplicationServicesAssembly,
                AssemblyFinder.InfrastructureAssembly
            }
        });
    }
}
