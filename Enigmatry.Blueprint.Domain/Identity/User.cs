using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Blueprint.Domain.Identity.Commands;
using Enigmatry.Blueprint.Domain.Identity.DomainEvents;
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Blueprint.Domain.Identity;

public class User : EntityWithGuidId, IEntityHasCreatedUpdated
{
    public const int NameMinLength = 5;
    public const int NameMaxLength = 25;

    public string UserName { get; private set; } = "";
    public string Name { get; private set; } = "";
    public DateTimeOffset CreatedOn { get; private set; }
    public DateTimeOffset UpdatedOn { get; private set; }
    public Guid? CreatedById { get; private set; }
    public Guid? UpdatedById { get; private set; }

    public User? CreatedBy { get; private set; }
    public User? UpdatedBy { get; private set; }

    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = null!;

    public static User Create(CreateOrUpdateUser.Command command)
    {
        var result = new User
        {
            UserName = command.UserName,
            Name = command.Name,
            RoleId = command.RoleId
        };

        result.AddDomainEvent(new UserCreatedDomainEvent(result.UserName));
        return result;
    }

    public void Update(CreateOrUpdateUser.Command command)
    {
        Name = command.Name;
        RoleId = command.RoleId;
        AddDomainEvent(new UserUpdatedDomainEvent(UserName));
    }

    public IEnumerable<PermissionId> GetPermissionIds() => Role.Permissions.Select(p => p.Id);

    public void SetCreated(DateTimeOffset createdOn, Guid createdBy)
    {
        SetCreated(createdOn);
        CreatedById = createdBy;
    }

    public void SetCreated(DateTimeOffset createdOn) => CreatedOn = createdOn;

    public void SetUpdated(DateTimeOffset updatedOn, Guid updatedBy)
    {
        SetUpdated(updatedOn);
        UpdatedById = updatedBy;
    }

    public void SetUpdated(DateTimeOffset updatedOn) => UpdatedOn = updatedOn;

    public bool HasAnyPermission(IEnumerable<PermissionId> permissionIds) => GetPermissionIds().Intersect(permissionIds).Any();
}
