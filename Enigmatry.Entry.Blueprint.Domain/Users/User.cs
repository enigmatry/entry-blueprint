using Enigmatry.Entry.Blueprint.Core.Entities;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Domain.Users.Commands;
using Enigmatry.Entry.Blueprint.Domain.Users.DomainEvents;

namespace Enigmatry.Entry.Blueprint.Domain.Users;

public class User : EntityWithCreatedUpdated
{
    public const int NameMaxLength = 200;
    public const int EmailAddressMaxLength = 200;
    public static readonly Guid SystemUserId = new("DFB44AA8-BFC9-4D95-8F45-ED6DA241DCFC");
    public string EmailAddress { get; private set; } = String.Empty;
    public string FullName { get; private set; } = String.Empty;
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = null!;
    public UserStatusId StatusId { get; private set; } = null!;
    public UserStatus Status { get; private set; } = null!;

    public static User Create(CreateOrUpdateUser.Command command)
    {
        var result = new User
        {
            EmailAddress = command.EmailAddress, FullName = command.FullName, RoleId = command.RoleId, StatusId = command.StatusId
        };

        result.AddDomainEvent(new UserCreatedDomainEvent(result.EmailAddress));
        return result;
    }

    public void Update(CreateOrUpdateUser.Command command)
    {
        FullName = command.FullName;
        RoleId = command.RoleId;
        StatusId = command.StatusId;
        AddDomainEvent(new UserUpdatedDomainEvent(EmailAddress));
    }

    public void UpdateRole(Role role)
    {
        RoleId = role.Id;
        Role = role;
        AddDomainEvent(new UserUpdatedDomainEvent(EmailAddress));
    }

    public IReadOnlyCollection<PermissionId> GetPermissionIds() => Role.Permissions.Select(p => p.Id).ToArray();
}
