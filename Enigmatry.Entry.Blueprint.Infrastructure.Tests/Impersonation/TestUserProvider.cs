using Enigmatry.Entry.Blueprint.Core.Logging;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Blueprint.Infrastructure.Identity;
using Enigmatry.Entry.Core.Data;
using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Tests.Impersonation;

[UsedImplicitly]
public class TestUserProvider(IRepository<User> userRepository, ISecurityLogger<SystemUserProvider> logger)
    : SystemUserProvider(userRepository, logger)
{
    public override Guid? UserId => TestUserData.TestUserId;
}
