using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Blueprint.Domain.Tests.Authorization;

public class RoleBuilder
{
    private string _name = String.Empty;
    private ICollection<Permission>? _permissions;
    private Guid _id = SequentialGuidGenerator.Generate();

    public RoleBuilder WithName(string value)
    {
        _name = value;
        return this;
    }

    public RoleBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public RoleBuilder WithPermissions(ICollection<Permission> permissions)
    {
        _permissions = permissions;
        return this;
    }

    public static implicit operator Role(RoleBuilder builder) => ToRole(builder);

    public static Role ToRole(RoleBuilder builder) => builder.Build();

    public Role Build()
    {
        var result = Role.Create(_name, _permissions).WithId(_id);
        return result;
    }
}
