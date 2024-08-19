using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Blueprint.Infrastructure.Identity;
using Enigmatry.Entry.Core.Data;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Tests.Impersonation;

[UsedImplicitly]
public class TestUserProvider(IRepository<User> userRepository, ILogger<TestUserProvider> logger)
    : SystemUserProvider(userRepository, logger)
{
    public override Guid? UserId => TestUserData.TestUserId;
}
