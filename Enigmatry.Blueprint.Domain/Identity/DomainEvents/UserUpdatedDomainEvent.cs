using Enigmatry.Blueprint.Domain.Auditing;

namespace Enigmatry.Blueprint.Domain.Identity.DomainEvents;

public record UserUpdatedDomainEvent : AuditableDomainEvent
{
    public UserUpdatedDomainEvent(string userName) : base("UserUpdated")
    {
        UserName = userName;
    }

    public string UserName { get; }

    public override object AuditPayload => new { UserName };
}
