using System;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Blueprint.Model.Identity
{
    [PublicAPI]
    public class UserCreateOrUpdateCommand : IRequest<User>
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
    }
}