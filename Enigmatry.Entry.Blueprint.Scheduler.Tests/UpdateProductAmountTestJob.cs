using Enigmatry.Entry.Blueprint.Scheduler.Jobs;
using Enigmatry.Entry.Blueprint.Scheduler.Jobs.Requests;
using Enigmatry.Entry.Core.Data;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Entry.Blueprint.Scheduler.Tests;

public class UpdateProductAmountTestJob : UpdateProductAmountJob
{
    public UpdateProductAmountTestJob(ILogger<UpdateProductAmountTestJob> logger, IConfiguration configuration, IMediator mediator, IUnitOfWork unitOfWork) : base(logger, configuration, mediator, unitOfWork)
    {
    }

    public override async Task Execute(EmptyJobRequest request)
    {
        ExecutedRequest = request;
        await base.Execute(request);
    }

    internal EmptyJobRequest? ExecutedRequest { get; private set; }
}
