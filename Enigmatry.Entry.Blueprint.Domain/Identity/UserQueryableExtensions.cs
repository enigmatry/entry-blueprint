using Enigmatry.Entry.Blueprint.Domain.Users;

namespace Enigmatry.Entry.Blueprint.Domain.Identity;

public static class UserQueryableExtensions
{
    public static IQueryable<User> QueryByName(this IQueryable<User> query, string? name) =>
        !String.IsNullOrEmpty(name)
            ? query.Where(e => e.FullName.Contains(name))
            : query;

    public static IQueryable<User> QueryByEmail(this IQueryable<User> query, string? email) =>
        !String.IsNullOrEmpty(email)
            ? query.Where(e => e.EmailAddress.Contains(email))
            : query;
}
