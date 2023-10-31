using Enigmatry.Blueprint.Domain.Auditing;
using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Blueprint.Domain.Users.DomainEvents;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.ApplicationServices.Auditing;

[UsedImplicitly]
public class AuditableDomainEventNotificationHandler<T> : INotificationHandler<T> where T : AuditableDomainEvent
{
    private readonly ILogger<AuditableDomainEventNotificationHandler<T>> _log;
    private readonly ICurrentUserProvider _currentUserProvider;

    public AuditableDomainEventNotificationHandler(ILogger<AuditableDomainEventNotificationHandler<T>> log, ICurrentUserProvider currentUserProvider)
    {
        _log = log;
        _currentUserProvider = currentUserProvider;
    }

    public Task Handle(T notification, CancellationToken cancellationToken)
    {
        // here you can enter record in Audit table, 
        _log.LogDebug("Event name: {EventName}, Payload: {@Payload}, initiated by: {EmailAddress}", notification.EventName, notification.AuditPayload, _currentUserProvider.User?.EmailAddress);
        return Task.CompletedTask;
    }
}
