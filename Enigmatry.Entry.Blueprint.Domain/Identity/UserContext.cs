namespace Enigmatry.Entry.Blueprint.Domain.Identity;

public class UserContext(Guid id, PermissionsContext permissions)
{
    public Guid Id => id;
    public PermissionsContext Permissions => permissions;
}
