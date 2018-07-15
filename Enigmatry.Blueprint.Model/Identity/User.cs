using System;
using Enigmatry.Blueprint.Core;

namespace Enigmatry.Blueprint.Model.Identity
{
    public class User : Entity
    {
        public string UserName { get; private set; }
        public string Name { get; private set; }
        public DateTimeOffset CreatedOn { get; private set; }

        public static User Create(UserCreateDto userCreateDto)
        {
            return new User
            {
                UserName = userCreateDto.UserName,
                Name = userCreateDto.Name,
                CreatedOn = userCreateDto.CreatedOn
            };
        }
    }
}