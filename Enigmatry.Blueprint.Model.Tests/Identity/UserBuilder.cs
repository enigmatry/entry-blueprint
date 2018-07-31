using System;
using Enigmatry.Blueprint.Model.Identity;

namespace Enigmatry.Blueprint.Model.Tests.Identity
{
    public class UserBuilder
    {
        private DateTimeOffset _createdOn;
        private string _name;
        private DateTimeOffset _updatedOn;
        private string _userName;

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

        public UserBuilder CreatedOn(DateTimeOffset value)
        {
            _createdOn = value;
            return this;
        }

        public UserBuilder UpdatedOn(DateTimeOffset value)
        {
            _updatedOn = value;
            return this;
        }

        public static implicit operator User(UserBuilder builder)
        {
            return builder.Build();
        }

        private User Build()
        {
            User result = User.Create(new UserCreateOrUpdateCommand
            {
                Name = _name,
                UserName = _userName
            });

            result.SetCreated(_createdOn);
            result.SetUpdated(_updatedOn);

            return result;
        }
    }
}