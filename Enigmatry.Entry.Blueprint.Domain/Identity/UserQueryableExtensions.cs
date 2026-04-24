using Enigmatry.Entry.Blueprint.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Domain.Identity;

public static class UserQueryableExtensions
{
    extension(IQueryable<User> query)
    {
        public IQueryable<User> BuildAggregateInclude() =>
            query
                .Include(u => u.UserStatus)
                .Include(u => u.Role)
                .ThenInclude(r => r.Permissions);

        public IQueryable<User> QueryByName(string? name) =>
            name is null or "" ? query : query.Where(e => e.FullName.Contains(name));

        public IQueryable<User> QueryByEmail(string? email) =>
            email is null or "" ? query : query.Where(e => e.EmailAddress.Contains(email));
    }
}
