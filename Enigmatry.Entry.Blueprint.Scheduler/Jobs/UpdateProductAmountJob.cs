using Enigmatry.Entry.Blueprint.Domain.Products;
using Enigmatry.Entry.Blueprint.Domain.Products.Commands;
using Enigmatry.Entry.Blueprint.Scheduler.Jobs.Requests;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Scheduler;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Entry.Blueprint.Scheduler.Jobs;

[UsedImplicitly]
public class UpdateProductAmountJob : EntryJob<EmptyJobRequest>
{
    private readonly IMediator _mediator;
    private readonly ILogger<UpdateProductAmountJob> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductAmountJob(ILogger<UpdateProductAmountJob> logger,
        IConfiguration configuration,
        IMediator mediator,
        IUnitOfWork unitOfWork) : base(logger, configuration)
    {
        _logger = logger;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public override async Task Execute(EmptyJobRequest request)
    {
        _logger.LogInformation("Scheduled job executed at {Now}", DateTimeOffset.Now);
        await _mediator.Send(new ProductUpdateAmount.Command { Id = Product.TestProductId });
        await _unitOfWork.SaveChangesAsync();
    }
}
