using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.EntityFramework;
using Enigmatry.Entry.SmartEnums.EntityFramework;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Data;

[UsedImplicitly]
public class AppDbContext(DbContextOptions options) : BaseDbContext(CreateOptions(), options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) =>
        configurationBuilder.Properties<string>()
            .HaveMaxLength(255);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // first we need to build the model so that we can later configure the smart enums
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyEntityWithGuidIdConvention();
        modelBuilder.EntryConfigureSmartEnums();
    }

    private static EntitiesDbContextOptions CreateOptions() =>
        new() { ConfigurationAssembly = AssemblyFinder.InfrastructureAssembly, EntitiesAssembly = AssemblyFinder.DomainAssembly };
}
