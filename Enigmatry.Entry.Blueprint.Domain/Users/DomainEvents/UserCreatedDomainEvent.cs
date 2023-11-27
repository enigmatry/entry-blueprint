using Enigmatry.Entry.Blueprint.Domain.Auditing;

namespace Enigmatry.Entry.Blueprint.Domain.Users.DomainEvents;

public record UserCreatedDomainEvent(string EmailAddress) : AuditableDomainEvent("UserCreated")
{
    public override object AuditPayload => new { EmailAddress };
}
