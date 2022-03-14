using Enigmatry.Blueprint.Model.Auditing;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.ApplicationServices.Auditing;

[UsedImplicitly]
public class AuditableDomainEventNotificationHandler : INotificationHandler<AuditableDomainEvent>
{
    private readonly ILogger<AuditableDomainEventNotificationHandler> _log;
    private readonly ICurrentUserProvider _currentUserProvider;

    public AuditableDomainEventNotificationHandler(ILogger<AuditableDomainEventNotificationHandler> log, ICurrentUserProvider currentUserProvider)
    {
        _log = log;
        _currentUserProvider = currentUserProvider;
    }

    public Task Handle(AuditableDomainEvent notification, CancellationToken cancellationToken)
    {
        // here you can enter record in Audit table, 
        _log.LogDebug("Event name: {EventName}, Payload: {@Payload}, initiated by: {UserName}", notification.EventName, notification.AuditPayload, _currentUserProvider.User?.UserName);
        return Task.CompletedTask;
    }
}
