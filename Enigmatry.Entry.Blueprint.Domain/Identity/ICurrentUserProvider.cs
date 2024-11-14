namespace Enigmatry.Entry.Blueprint.Domain.Identity;

public interface ICurrentUserProvider
{
    public UserContext? User { get; }
    public Guid? UserId { get; }
}
