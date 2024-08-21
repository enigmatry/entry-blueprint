using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Core.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Identity;

[UsedImplicitly]
public class PrincipalProviderCurrentUserProvider(
    IClaimsProvider claimsProvider,
    IRepository<User> userRepository,
    ILogger<PrincipalProviderCurrentUserProvider> logger)
    : ICurrentUserProvider
{
    private UserContext? _user;
    private bool IsAuthenticated => claimsProvider.IsAuthenticated;
    public Guid? UserId => User?.Id;

    public UserContext? User
    {
        get
        {
            if (_user != null)
            {
                return _user;
            }

            if (!IsAuthenticated)
            {
                logger.LogWarning("User is not authenticated");
                return null;
            }

            if (String.IsNullOrEmpty(claimsProvider.Email))
            {
                logger.LogWarning("User's email was not found in the claims");
                return null;
            }

            var user = userRepository
                .QueryAll()
                .QueryByEmailAddress(claimsProvider.Email)
                .BuildAggregateInclude()
                .AsNoTracking()
                .AsSplitQuery()
                .SingleOrDefault();

            _user = user != null
                ? new UserContext(user.Id, new PermissionsContext(user.Role.Permissions.Select(x => x.Id).Distinct().ToArray()))
                : null;

            return _user;
        }
    }
}
