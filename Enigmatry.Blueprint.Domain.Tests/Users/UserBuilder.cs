using Enigmatry.Blueprint.Domain.Users;
using Enigmatry.Blueprint.Domain.Users.Commands;
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Blueprint.Domain.Tests.Users;

public class UserBuilder
{
    private string _fullName = String.Empty;
    private Guid _roleId = Guid.Empty;
    private string _emailAddress = String.Empty;
    private Guid _id = SequentialGuidGenerator.Generate();

    public UserBuilder WithEmailAddress(string value)
    {
        _emailAddress = value;
        return this;
    }

    public UserBuilder WithFullName(string value)
    {
        _fullName = value;
        return this;
    }

    public UserBuilder WithRoleId(Guid value)
    {
        _roleId = value;
        return this;
    }

    public UserBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public static implicit operator User(UserBuilder builder) => ToUser(builder);

    public static User ToUser(UserBuilder builder) => builder.Build();

    public User Build()
    {
        User result = User.Create(new CreateOrUpdateUser.Command
        {
            FullName = _fullName,
            EmailAddress = _emailAddress,
            RoleId = _roleId
        }).WithId(_id);

        return result;
    }
}
