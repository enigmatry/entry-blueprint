using MediatR;

namespace Enigmatry.Blueprint.Model.Auditing
{
    public class AuditableDomainEvent : INotification
    {
        public AuditableDomainEvent(string eventName, object payload)
        {
            EventName = eventName;
            Payload = payload;
        }

        public string EventName { get; }

        public object Payload { get; }
    }
}
