using Enigmatry.Entry.Core.Helpers;

namespace Enigmatry.Entry.Blueprint.Domain.Users;

public static class UserQueryableExtensions
{
    public static IQueryable<User> QueryByEmailAddress(this IQueryable<User> query, string emailAddress) => query.Where(e => e.EmailAddress == emailAddress);

    public static IQueryable<User> QueryByKeyword(this IQueryable<User> query, string keyword) =>
        keyword.HasContent()
            ? query.Where(e => e.EmailAddress.Contains(keyword))
            : query;
}
