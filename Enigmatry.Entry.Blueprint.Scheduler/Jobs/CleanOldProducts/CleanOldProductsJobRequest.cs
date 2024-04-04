namespace Enigmatry.Entry.Blueprint.Scheduler.Jobs.Requests;

public class CleanOldProductsJobRequest
{
    public TimeSpan DeactivateOlderThan { get; init; }
}
