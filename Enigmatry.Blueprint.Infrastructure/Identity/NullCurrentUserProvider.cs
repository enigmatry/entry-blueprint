using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Blueprint.Domain.Users;

namespace Enigmatry.Blueprint.Infrastructure.Identity;

public class NullCurrentUserProvider : ICurrentUserProvider
{
    public User? User => null;
    public bool IsAuthenticated => false;
}
