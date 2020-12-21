using Enigmatry.Blueprint.BuildingBlocks.Core.Entities;

namespace Enigmatry.Blueprint.Model.Auditing
{
    public abstract record AuditableDomainEvent : DomainEvent
    {
        protected AuditableDomainEvent(string eventName)
        {
            EventName = eventName;
        }

        public string EventName { get; }

        public abstract object AuditPayload { get; }
    }
}
