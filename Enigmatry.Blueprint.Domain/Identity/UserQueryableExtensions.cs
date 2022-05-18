using Enigmatry.BuildingBlocks.Core.Helpers;

namespace Enigmatry.Blueprint.Domain.Identity;

public static class UserQueryableExtensions
{
    public static IQueryable<User> QueryByUserName(this IQueryable<User> query, string username) => query.Where(e => e.UserName == username);

    public static IQueryable<User> QueryByKeyword(this IQueryable<User> query, string keyword) =>
        keyword.HasContent()
            ? query.Where(e => e.UserName.Contains(keyword))
            : query;
}
