using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Enigmatry.Blueprint.Infrastructure.MediatR
{
    public class NoMediator : IMediator
    {
        public Task Publish(object notification, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public Task Publish<TNotification>(TNotification notification,
            CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
            CancellationToken cancellationToken = default)
        {
#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
            return Task.FromResult(default(TResponse));
#pragma warning restore CS8653 // A default expression introduces a null value for a type parameter.
        }
    }
}
