using MediatR;

namespace Enigmatry.Blueprint.Model.Auditing
{
    public class AuditableDomainEvent : INotification
    {
        public AuditableDomainEvent(string eventName, dynamic payload)
        {
            EventName = eventName;
            Payload = payload;
        }

        public string EventName { get; }

        public dynamic Payload { get; }
    }
}
