namespace Enigmatry.Blueprint.Domain.Identity;

public interface ICurrentUserProvider
{
    Guid? UserId { get; }
    User? User { get; }
    bool IsAuthenticated { get; }
}
