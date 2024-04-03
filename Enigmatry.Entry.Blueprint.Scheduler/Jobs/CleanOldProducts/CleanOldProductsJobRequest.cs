namespace Enigmatry.Entry.Blueprint.Scheduler.Jobs.CleanOldProducts;

public class CleanOldProductsJobRequest
{
    public TimeSpan DeactivateOlderThan { get; init; }
}
