using System;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Model.Identity
{
    [PublicAPI]
    public class UserCreateOrUpdateCommand
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
    }
}