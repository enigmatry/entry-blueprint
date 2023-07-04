using Enigmatry.Entry.Core.Entities;
using Enigmatry.Blueprint.Domain.Authorization;

namespace Enigmatry.Blueprint.Domain.Tests.Authorization;

public class PermissionBuilder
{
    private string _name = String.Empty;
    private Guid _id = SequentialGuidGenerator.Generate();

    public PermissionBuilder WithName(string value)
    {
        _name = value;
        return this;
    }

    public PermissionBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public static implicit operator Permission(PermissionBuilder builder) => ToPermission(builder);

    public static Permission ToPermission(PermissionBuilder builder) => builder.Build();

    public Permission Build()
    {
        var result = Permission.Create(_name).WithId(_id);

        return result;
    }
}
