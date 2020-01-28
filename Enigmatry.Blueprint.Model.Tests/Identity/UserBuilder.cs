using System;
using Enigmatry.Blueprint.Model.Identity;
using Enigmatry.Blueprint.Model.Identity.Commands;

namespace Enigmatry.Blueprint.Model.Tests.Identity
{
    public class UserBuilder
    {
        private string _name = String.Empty;
        private string _userName = String.Empty;

        public UserBuilder UserName(string value)
        {
            _userName = value;
            return this;
        }

        public UserBuilder Name(string value)
        {
            _name = value;
            return this;
        }

        public static implicit operator User(UserBuilder builder)
        {
            return builder.Build();
        }

        private User Build()
        {
            User result = User.Create(new UserCreateOrUpdate.Command
            {
                Name = _name,
                UserName = _userName
            });

            return result;
        }
    }
}
