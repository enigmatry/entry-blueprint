using MediatR;

namespace Enigmatry.Blueprint.Model.Auditing
{
    public abstract class AuditableDomainEvent : INotification
    {
        protected AuditableDomainEvent(string eventName)
        {
            EventName = eventName;
        }

        public string EventName { get; }

        public abstract object AuditPayload { get; }
    }
}
