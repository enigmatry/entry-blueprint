using Enigmatry.Entry.Blueprint.Scheduler.Jobs.Requests;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.MediatR;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

namespace Enigmatry.Entry.Blueprint.Scheduler.Tests;

[Category("unit")]
public class UpdateProductAmountJobFixture
{
    private readonly IUnitOfWork _unitOfWork = A.Fake<IUnitOfWork>();

    [Test]
    public async Task TestExecute()
    {
        var job = new UpdateProductAmountTestJob(NullLogger<UpdateProductAmountTestJob>.Instance, A.Fake<IConfiguration>(),
            new NullMediator(), _unitOfWork);

        await job.Execute(new EmptyJobRequest());

        await Verify(job.ExecutedRequest);
        A.CallTo(() => _unitOfWork.SaveChangesAsync(A<CancellationToken>._)).MustHaveHappenedOnceExactly();
    }
}
