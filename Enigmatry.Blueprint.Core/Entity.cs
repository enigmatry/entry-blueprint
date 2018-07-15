using System.Collections.Generic;
using MediatR;

namespace Enigmatry.Blueprint.Core
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public List<INotification> DomainEvents { get; private set; }

        protected void AddDomainEvent(INotification eventItem)
        {
            DomainEvents = DomainEvents ?? new List<INotification>();
            DomainEvents.Add(eventItem);
        }
    }
}