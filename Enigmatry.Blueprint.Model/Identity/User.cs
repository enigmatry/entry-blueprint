using System;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Model.Auditing;

namespace Enigmatry.Blueprint.Model.Identity
{
    public class User : Entity, IEntityHasCreatedUpdated
    {
        public string UserName { get; private set; }
        public string Name { get; private set; }
        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset UpdatedOn { get; private set; }

        public static User Create(UserCreateOrUpdateCommand command)
        {
            var result = new User
            {
                UserName = command.UserName,
                Name = command.Name,
            };

            result.AddDomainEvent(new UserCreatedDomainEvent(result.UserName));
            result.AddDomainEvent(new AuditableDomainEvent("User created", new {result.UserName}));

            return result;
        }

        public void Update(UserCreateOrUpdateCommand command)
        {
            UserName = command.UserName;
            Name = command.Name;

            AddDomainEvent(new UserUpdatedDomainEvent(UserName));
        }

        public void SetCreated(DateTimeOffset createdOn, Guid createdBy)
        {
            CreatedOn = createdOn;
        }

        public void SetUpdated(DateTimeOffset updatedOn, Guid updatedBy)
        {
            UpdatedOn = updatedOn;
        }
    }
}