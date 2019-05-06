using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Infrastructure.MediatR
{
    public class SamplePostRequestBehavior<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly ILogger<SamplePostRequestBehavior<TRequest, TResponse>> _logger;

        public SamplePostRequestBehavior(ILogger<SamplePostRequestBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Post processing");
            return Task.CompletedTask;
        }
    }
}
