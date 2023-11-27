using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Entry.Blueprint.Domain.Auditing;

public abstract record AuditableDomainEvent(string EventName) : DomainEvent
{
    public abstract object AuditPayload { get; }
}
