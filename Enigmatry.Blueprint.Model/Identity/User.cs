using System;
using Enigmatry.Blueprint.Core;

namespace Enigmatry.Blueprint.Model.Identity
{
    public class User : Entity
    {
        public string UserName { get; private set; }
        public string Name { get; private set; }
        public DateTimeOffset CreatedOn { get; private set; }

        public static User Create(string userName, string someName, DateTimeOffset createdOn)
        {
            return new User {UserName = userName, Name = someName, CreatedOn = createdOn};
        }
    }
}