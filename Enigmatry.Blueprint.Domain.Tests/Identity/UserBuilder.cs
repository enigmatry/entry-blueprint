using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Blueprint.Domain.Identity.Commands;
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Blueprint.Model.Tests.Identity;

public class UserBuilder
{
    private string _name = String.Empty;
    private Guid _roleId = Guid.Empty;
    private string _userName = String.Empty;
    private Guid _id = SequentialGuidGenerator.Generate();

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
            Name = _name,
            UserName = _userName,
            RoleId = _roleId
        }).WithId(_id);

        return result;
    }
}
