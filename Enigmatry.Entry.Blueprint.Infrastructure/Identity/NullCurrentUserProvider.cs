using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Blueprint.Domain.Users;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Identity;

public class NullCurrentUserProvider : ICurrentUserProvider
{
    public User? User => null;
    public bool IsAuthenticated => false;
}
