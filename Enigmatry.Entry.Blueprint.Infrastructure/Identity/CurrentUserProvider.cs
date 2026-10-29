using Enigmatry.Entry.Blueprint.Core.Logging;
using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Core.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Identity;

[UsedImplicitly]
public class CurrentUserProvider(
    IClaimsProvider claimsProvider,
    IRepository<User> userRepository,
    ISecurityLogger<CurrentUserProvider> logger)
    : ICurrentUserProvider
{
    private bool IsAuthenticated => claimsProvider.IsAuthenticated;
    public Guid? UserId => User?.UserId;

    public UserContext? User
    {
        get
        {
            if (field != null)
            {
                return field;
            }

            if (!IsAuthenticated)
            {
                logger.LogSecurityWarning("User is not authenticated");
                return null;
            }

            if (String.IsNullOrEmpty(claimsProvider.Email))
            {
                logger.LogSecurityWarning("User's email was not found in the claims");
                return null;
            }

            var user = userRepository
                .QueryAll()
                .QueryByEmailAddress(claimsProvider.Email)
                .BuildAggregateInclude()
                .AsNoTracking()
                .AsSplitQuery()
                .SingleOrDefault();

            if (user == null)
            {
                logger.LogSecurityWarning("Authenticated user with email {EmailAddress} was not found in the database", claimsProvider.Email);
                return null;
            }

            field = new UserContext(user.Id, new PermissionsContext(user.Role.Permissions.Select(x => x.Id).Distinct().ToArray()));

            return field;
        }
    }
}
