using Enigmatry.Entry.Blueprint.Domain.Products.Commands;
using Enigmatry.Entry.Blueprint.Scheduler.Jobs.Requests;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Scheduler;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Entry.Blueprint.Scheduler.Jobs;

[UsedImplicitly]
public class UpdateProductAmountJob : EntryJob<UpdateProductAmountJobRequest>
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateProductAmountJob> _logger;

    public UpdateProductAmountJob(ILogger<UpdateProductAmountJob> logger,
        IConfiguration configuration,
        IMediator mediator,
        IUnitOfWork unitOfWork) : base(logger, configuration)
    {
        _logger = logger;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public override async Task Execute(UpdateProductAmountJobRequest request)
    {
        _logger.LogInformation("Updating product amount for product {ProductId} to {Amount}", request.ProductId, request.Amount);
        var command = MapToCommand(request);
        await _mediator.Send(command);
        await _unitOfWork.SaveChangesAsync();
    }

    private static ProductUpdateAmount.Command MapToCommand(UpdateProductAmountJobRequest request) =>
        new() { ProductId = request.ProductId, Amount = request.Amount };
}
