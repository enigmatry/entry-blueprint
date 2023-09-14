using Enigmatry.Blueprint.Domain.Auditing;

namespace Enigmatry.Blueprint.Domain.Users.DomainEvents;

public record UserCreatedDomainEvent(string EmailAddress) : AuditableDomainEvent("UserCreated")
{
    public override object AuditPayload => new { EmailAddress };
}
