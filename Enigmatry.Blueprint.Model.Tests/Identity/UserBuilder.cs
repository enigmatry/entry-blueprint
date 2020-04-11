using System;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Model.Identity;
using Enigmatry.Blueprint.Model.Identity.Commands;

namespace Enigmatry.Blueprint.Model.Tests.Identity
{
    public class UserBuilder
    {
        private string _name = String.Empty;
        private string _userName = String.Empty;
        private Guid _id = SequentialGuidValueGenerator.Generate();

        public UserBuilder WithUserName(string value)
        {
            _userName = value;
            return this;
        }

        public UserBuilder WithName(string value)
        {
            _name = value;
            return this;
        }

        public UserBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public static implicit operator User(UserBuilder builder)
        {
            return builder.Build();
        }

        public User Build()
        {
            User result = User.Create(new UserCreateOrUpdate.Command
            {
                Name = _name,
                UserName = _userName
            }).WithId(_id);

            return result;
        }
    }
}
