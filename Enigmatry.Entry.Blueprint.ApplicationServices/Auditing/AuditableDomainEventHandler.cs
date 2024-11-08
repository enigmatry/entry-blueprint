using Enigmatry.Entry.Blueprint.Domain.Auditing;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Entry.Blueprint.ApplicationServices.Auditing;

[UsedImplicitly]
public class AuditableDomainEventNotificationHandler<T>(
    ILogger<AuditableDomainEventNotificationHandler<T>> log)
    : INotificationHandler<T>
    where T : AuditableDomainEvent
{
    public Task Handle(T notification, CancellationToken cancellationToken)
    {
        // here you can enter record in Audit table, 
        log.LogDebug("Event name: {EventName}, Payload: {@Payload}", notification.EventName, notification.AuditPayload);
        return Task.CompletedTask;
    }
}
