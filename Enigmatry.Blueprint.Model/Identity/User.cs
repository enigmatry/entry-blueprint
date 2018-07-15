﻿using System;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Model.Auditing;

namespace Enigmatry.Blueprint.Model.Identity
{
    public class User : Entity, IEntityHasCreatedUpdated
    {
        public string UserName { get; private set; }
        public string Name { get; private set; }
        public DateTimeOffset CreatedOn { get; private set; }

        public static User Create(UserCreateDto userCreateDto)
        {
            var result = new User
            {
                UserName = userCreateDto.UserName,
                Name = userCreateDto.Name,
            };

            result.AddDomainEvent(new AuditableDomainEvent("User created", new {result.UserName}));

            return result;
        }

        public void SetCreated(DateTimeOffset createdOn, int createdBy)
        {
            CreatedOn = createdOn;
        }
        
        public void SetUpdated(DateTimeOffset updatedOn, int updatedBy)
        {
        }
    }
}