using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Enigmatry.Blueprint.Infrastructure.MediatR
{
    public class NoMediator : IMediator
    {
        public Task Publish(object notification, CancellationToken cancellationToken = new CancellationToken()) => Task.CompletedTask;

        public Task Publish<TNotification>(TNotification notification,
            CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(default(TResponse)!);
        }

        public Task<object?> Send(object request, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult(default(object));
        }
    }
}
