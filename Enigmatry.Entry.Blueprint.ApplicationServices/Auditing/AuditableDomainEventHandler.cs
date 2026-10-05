using Enigmatry.Entry.Blueprint.Core.Logging;
using Enigmatry.Entry.Blueprint.Domain.Auditing;
using Enigmatry.Entry.Blueprint.Domain.Identity;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Entry.Blueprint.ApplicationServices.Auditing;

[UsedImplicitly]
public class AuditableDomainEventNotificationHandler<T>(
    ISecurityLogger<AuditableDomainEventNotificationHandler<T>> logger,
    ICurrentUserProvider currentUserProvider)
    : INotificationHandler<T>
    where T : AuditableDomainEvent
{
    public Task Handle(T notification, CancellationToken cancellationToken)
    {
        // here you can enter record in Audit table,
        logger.LogSecurityInformation("Event name: {EventName}, Payload: {@Payload}, initiated by user: {UserId}",
            notification.EventName, notification.AuditPayload, currentUserProvider.UserId);
        return Task.CompletedTask;
    }
}
