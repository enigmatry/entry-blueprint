using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.Entities;
using Enigmatry.Entry.Core.Logging;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DomainUser = Enigmatry.Entry.Blueprint.Domain.Users.User;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Identity;

[UsedImplicitly]
public class SystemUserProvider(IRepository<DomainUser> userRepository, ILogger<SystemUserProvider> logger) : ICurrentUserProvider
{
    public virtual Guid? UserId => DomainUser.SystemUserId;

    public UserContext? User
    {
        get
        {
            if (field != null)
            {
                return field;
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
                logger.LogSecurityWarning("User with Id: {SystemUserId} not found in the database. This user might not have been seeded", UserId);
                return null;
            }

            field = new UserContext(user.Id, new PermissionsContext(user.GetPermissionIds()));

            return field;
        }
    }
}
