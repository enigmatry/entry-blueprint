using System;
using System.ComponentModel;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Blueprint.Model.Identity
{
    [PublicAPI]
    public class UserCreateOrUpdateCommand : IRequest<User>
    {
        public Guid? Id { get; set; }

        [DisplayName("Username")]
        public string UserName { get; set; }
        
        [DisplayName("Name")]
        public string Name { get; set; }

        public bool IsCreate => !Id.HasValue;
    }
}