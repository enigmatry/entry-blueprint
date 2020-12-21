using Enigmatry.Blueprint.Model.Auditing;

namespace Enigmatry.Blueprint.Model.Identity.DomainEvents
{
    public record UserUpdatedDomainEvent : AuditableDomainEvent
    {
        public UserUpdatedDomainEvent(string userName) : base("UserUpdated")
        {
            UserName = userName;
        }

        public string UserName { get; }

        public override object AuditPayload => new { UserName };
    }
}
