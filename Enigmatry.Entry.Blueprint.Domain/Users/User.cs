using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Domain.Users.Commands;
using Enigmatry.Entry.Blueprint.Domain.Users.DomainEvents;

namespace Enigmatry.Entry.Blueprint.Domain.Users;

public class User : EntityWithCreatedUpdated
{
    public const int NameMaxLength = 200;
    public const int EmailAddressMaxLength = 200;

    public string EmailAddress { get; private set; } = String.Empty;
    public string FullName { get; private set; } = String.Empty;
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = null!;
    public string CreditCardNumber { get; set; } = null!;

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

    public bool HasAnyPermission(IEnumerable<PermissionId> permissionIds) => GetPermissionIds().Intersect(permissionIds).Any();
}
