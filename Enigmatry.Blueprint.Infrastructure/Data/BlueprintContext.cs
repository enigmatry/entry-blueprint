using Enigmatry.Entry.EntityFramework;
using Enigmatry.Entry.EntityFramework.Security;
using Enigmatry.Blueprint.Infrastructure.Api.Init;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Enigmatry.Entry.EntityFramework.MediatR;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Infrastructure.Data;

[UsedImplicitly]
public class BlueprintContext : MediatRDbContext
{
    public BlueprintContext(DbContextOptions options,
        IMediator mediator,
        IDbContextAccessTokenProvider dbContextAccessTokenProvider,
        ILogger<BlueprintContext> logger) :
        base(CreateOptions(), options, mediator, logger, dbContextAccessTokenProvider)
    {
    }

    private static EntitiesDbContextOptions CreateOptions() => new()
    {
        ConfigurationAssembly = AssemblyFinder.InfrastructureAssembly,
        EntitiesAssembly = AssemblyFinder.DomainAssembly
    };
}
