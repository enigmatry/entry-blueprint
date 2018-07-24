using System;
using System.Collections.Generic;
using MediatR;

namespace Enigmatry.Blueprint.Core
{
    public abstract class Entity
    {
        // needs to be private so that EF does not map the field
        private readonly List<INotification> _domainEvents = new List<INotification>();

        public IEnumerable<INotification> DomainEvents => _domainEvents;

        public Guid Id { get; set; }

        protected void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}