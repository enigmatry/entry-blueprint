namespace Enigmatry.Blueprint.Domain.Identity;

public interface ICurrentUserProvider
{
    User? User { get; }
    bool IsAuthenticated { get; }
}
