using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Infrastructure.MediatR
{
    public class PreRequestBehavior<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<PreRequestBehavior<TRequest>> _logger;

        public PreRequestBehavior(ILogger<PreRequestBehavior<TRequest>> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Pre processing");
            return Task.CompletedTask;
        }
    }
}