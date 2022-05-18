using Enigmatry.Blueprint.Domain.Auditing;

namespace Enigmatry.Blueprint.Domain.Identity.DomainEvents;

public record UserCreatedDomainEvent : AuditableDomainEvent
{
    public UserCreatedDomainEvent(string userName) : base("UserCreated")
    {
        UserName = userName;
    }

    public string UserName { get; }

    public override object AuditPayload => new { UserName };
}
