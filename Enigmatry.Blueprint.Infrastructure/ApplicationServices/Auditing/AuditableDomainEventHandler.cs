using System.Threading;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Model.Auditing;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Infrastructure.ApplicationServices.Auditing
{
    [UsedImplicitly]
    public class AuditableDomainEventHandler : INotificationHandler<AuditableDomainEvent>
    {
        private readonly ILogger<AuditableDomainEventHandler> _log;
        private readonly ICurrentUserProvider _currentUserProvider;

        public AuditableDomainEventHandler(ILogger<AuditableDomainEventHandler> log, ICurrentUserProvider currentUserProvider)
        {
            _log = log;
            _currentUserProvider = currentUserProvider;
        }

        public Task Handle(AuditableDomainEvent notification, CancellationToken cancellationToken)
        {
            // here you can enter record in Audit table, 
            _log.LogDebug("Event name: {EventName}, Payload: {@Payload}, initiated by: {UserName}", notification.EventName, (object) notification.Payload, _currentUserProvider.User.UserName);
            return Task.CompletedTask;
        }
    }
}
