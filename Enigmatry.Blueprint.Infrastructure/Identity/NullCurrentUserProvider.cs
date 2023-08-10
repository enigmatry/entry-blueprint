using Enigmatry.Blueprint.Domain.Identity;

namespace Enigmatry.Blueprint.Infrastructure.Identity;

public class NullCurrentUserProvider : ICurrentUserProvider
{
    public User? User => null;
    public bool IsAuthenticated => false;
}
