namespace Enigmatry.Entry.Blueprint.Domain.Identity;

public class UserContext(Guid userId, PermissionsContext permissions)
{
    public Guid UserId => userId;
    public PermissionsContext Permissions => permissions;
}
