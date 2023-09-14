using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Blueprint.Domain.Users.Commands;
using Enigmatry.Blueprint.Domain.Users.DomainEvents;
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Blueprint.Domain.Users;

public class User : EntityWithGuidId, IEntityHasCreatedUpdated
{
    public const int NameMaxLength = 200;
    public const int EmailAddressMaxLength = 200;

    public string EmailAddress { get; private set; } = String.Empty;
    public string FullName { get; private set; } = String.Empty;
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
            EmailAddress = command.EmailAddress,
            FullName = command.FullName,
            RoleId = command.RoleId
        };

        result.AddDomainEvent(new UserCreatedDomainEvent(result.EmailAddress));
        return result;
    }

    public void Update(CreateOrUpdateUser.Command command)
    {
        FullName = command.FullName;
        RoleId = command.RoleId;
        AddDomainEvent(new UserUpdatedDomainEvent(EmailAddress));
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
