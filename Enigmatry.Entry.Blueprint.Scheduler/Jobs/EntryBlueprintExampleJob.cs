using Enigmatry.Entry.Blueprint.Scheduler.Jobs.Requests;
using Enigmatry.Entry.Scheduler;
using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.Scheduler.Jobs;

[UsedImplicitly]
public class EntryBlueprintExampleJob : EntryJob<EmptyJobRequest>
{
    private readonly ILogger<EntryBlueprintExampleJob> _logger;

    public EntryBlueprintExampleJob(ILogger<EntryBlueprintExampleJob> logger, IConfiguration configuration) : base(logger, configuration)
    {
        _logger = logger;
    }

    public override Task Execute(EmptyJobRequest request)
    {
        _logger.LogInformation("Scheduled job executed at {Now}", DateTimeOffset.Now);
        return Task.CompletedTask;
    }
}
