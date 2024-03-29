namespace Enigmatry.Entry.Blueprint.Scheduler.Jobs.Requests;

public class UpdateProductAmountJobRequest
{
    public Guid ProductId { get; init; }
    public int Amount { get; init; }
}
