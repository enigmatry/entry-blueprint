namespace Enigmatry.Blueprint.Domain.Identity;

public interface ICurrentUserIdProvider
{
    Guid? UserId { get; }
    bool IsAuthenticated { get; }
}
