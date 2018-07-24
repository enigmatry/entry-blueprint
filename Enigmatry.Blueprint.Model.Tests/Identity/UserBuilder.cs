using System;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Model.Identity;

namespace Enigmatry.Blueprint.Model.Tests.Identity
{
    public class UserBuilder
    {   
        private DateTimeOffset _createdOn;
        private string _name;
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

        public static implicit operator User(UserBuilder builder)
        {
            return builder.Build();
        }

        private User Build()
        {
            return User.Create(new UserCreateUpdateDto
                {
                    Name = _name,
                    UserName = _userName
                })
                .CreatedOn(_createdOn, Guid.Empty);
        }
    }
}