using System;
using System.Collections.Generic;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Model.Identity.Commands;
using Enigmatry.Blueprint.Model.Identity.DomainEvents;

namespace Enigmatry.Blueprint.Model.Identity
{
    public class User : Entity, IEntityHasCreatedUpdated
    {
        public string UserName { get; private set; } = "";
        public string Name { get; private set; } = "";
        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset UpdatedOn { get; private set; }
        public Guid? CreatedById { get; private set; }
        public Guid? UpdatedById { get; private set; }

        public User? CreatedBy { get; private set; }
        public User? UpdatedBy { get; private set; }

        public ICollection<User>? CreatedUsers { get; private set; }
        public ICollection<User>? UpdatedUsers { get; private set; }

        public static User Create(UserCreateOrUpdate.Command command)
        {
            var result = new User
            {
                UserName = command.UserName,
                Name = command.Name,
            };

            result.AddDomainEvent(new UserCreatedDomainEvent(result.UserName));
            return result;
        }

        public void Update(UserCreateOrUpdate.Command command)
        {
            Name = command.Name;
            AddDomainEvent(new UserUpdatedDomainEvent(UserName));
        }

        public void SetCreated(DateTimeOffset createdOn, Guid createdBy)
        {
            SetCreated(createdOn);
            CreatedById = createdBy;
        }

        public void SetCreated(DateTimeOffset createdOn) => CreatedOn = createdOn;

        public void SetUpdated(DateTimeOffset updatedOn, Guid updatedBy)
        {
            SetUpdated(updatedOn);
            UpdatedById = updatedBy;
        }

        public void SetUpdated(DateTimeOffset updatedOn) => UpdatedOn = updatedOn;
    }
}
