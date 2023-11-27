using Enigmatry.Entry.Blueprint.Domain.Users;

namespace Enigmatry.Entry.Blueprint.Domain.Identity;

public interface ICurrentUserProvider
{
    User? User { get; }
    bool IsAuthenticated { get; }
}
