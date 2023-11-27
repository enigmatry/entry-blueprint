using Enigmatry.Entry.EntityFramework;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Data;

[UsedImplicitly]
public class BlueprintContext : BaseDbContext
{
    public BlueprintContext(DbContextOptions options) :
        base(CreateOptions(), options)
    {
    }

    private static EntitiesDbContextOptions CreateOptions() =>
        new() { ConfigurationAssembly = AssemblyFinder.InfrastructureAssembly, EntitiesAssembly = AssemblyFinder.DomainAssembly };
}
