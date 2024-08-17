using Enigmatry.Entry.EntityFramework;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.SmartEnums.EntityFramework;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Data;

[UsedImplicitly]
public class AppDbContext(DbContextOptions options) : BaseDbContext(CreateOptions(), options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.EntryConfigureSmartEnum();
        configurationBuilder.Properties<string>()
            .HaveMaxLength(255);
    }

    private static EntitiesDbContextOptions CreateOptions() =>
        new() { ConfigurationAssembly = AssemblyFinder.InfrastructureAssembly, EntitiesAssembly = AssemblyFinder.DomainAssembly };
}
