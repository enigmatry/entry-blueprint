using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using DomainUser = Enigmatry.Entry.Blueprint.Domain.Users.User;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Identity;

[UsedImplicitly]
public class SystemUserProvider(IRepository<DomainUser> userRepository, ILogger<SystemUserProvider> logger) : ICurrentUserProvider
{
    public virtual Guid? UserId => DomainUser.SystemUserId;
    private UserContext? _user;

    public UserContext? User
    {
        get
        {
            if (_user != null)
            {
                return _user;
            }

            var user = userRepository
                .QueryAll()
                .QueryById(UserId!.Value)
                .BuildAggregateInclude()
                .AsNoTracking()
                .AsSplitQuery()
                .SingleOrDefault();

            if (user == null)
            {
                logger.LogWarning("User with Id: {SystemUserId} not found in the database. This user might not have been seeded", UserId);
                return null;
            }

            _user = new UserContext(user.Id, new PermissionsContext(user.GetPermissionIds()));

            return _user;
        }
    }
}
