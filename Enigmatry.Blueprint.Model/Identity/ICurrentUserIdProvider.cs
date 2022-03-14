namespace Enigmatry.Blueprint.Model.Identity;

public interface ICurrentUserIdProvider
{
    Guid? UserId { get; }
    bool IsAuthenticated { get; }
}
