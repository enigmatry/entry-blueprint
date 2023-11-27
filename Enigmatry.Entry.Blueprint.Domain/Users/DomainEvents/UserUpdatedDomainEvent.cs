using Enigmatry.Entry.Blueprint.Domain.Auditing;

namespace Enigmatry.Entry.Blueprint.Domain.Users.DomainEvents;

public record UserUpdatedDomainEvent(string EmailAddress) : AuditableDomainEvent("UserUpdated")
{
    public override object AuditPayload => new { EmailAddress };
}
