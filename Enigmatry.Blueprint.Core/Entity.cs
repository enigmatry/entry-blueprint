using System;
using System.Collections.Generic;
using MediatR;

namespace Enigmatry.Blueprint.Core
{
    public abstract class Entity
    {
        // needs to be private so that EF does not map the field
        private List<INotification> _domainEvents;

        public List<INotification> DomainEvents => _domainEvents;

        public Guid Id { get; set; }

        protected void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }
    }
}