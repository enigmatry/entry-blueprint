using System.Threading;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Model.Identity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Infrastructure.ApplicationServices.Identity
{
    public class UserCreatedDomainEventHandler: INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly ILogger<UserCreatedDomainEventHandler> _log;

        public UserCreatedDomainEventHandler(ILogger<UserCreatedDomainEventHandler> log)
        {
            _log = log;
        }

        public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            // here you can send welcom email to user
            _log.LogDebug("User created: {UserName}", notification.UserName);
            return Task.CompletedTask;
        }
    }
}
