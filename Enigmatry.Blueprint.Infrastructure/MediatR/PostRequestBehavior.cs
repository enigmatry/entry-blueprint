using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Infrastructure.MediatR
{
    public class PostRequestBehavior<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly ILogger<PostRequestBehavior<TRequest, TResponse>> _logger;

        public PostRequestBehavior(ILogger<PostRequestBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, TResponse response)
        {
            _logger.LogDebug("Post processing");
            return Task.CompletedTask;
        }
    }
}