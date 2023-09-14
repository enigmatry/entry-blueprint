using Enigmatry.Blueprint.Domain.Auditing;

namespace Enigmatry.Blueprint.Domain.Users.DomainEvents;

public record UserUpdatedDomainEvent(string EmailAddress) : AuditableDomainEvent("UserUpdated")
{
    public override object AuditPayload => new { EmailAddress };
}
