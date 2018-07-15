using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Enigmatry.Blueprint.Infrastructure.MediatR
{
    public class NoMediator : IMediator
    {
        public Task Publish<TNotification>(TNotification notification,
            CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(default(TResponse));
        }

        public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.CompletedTask;
        }
    }
}
