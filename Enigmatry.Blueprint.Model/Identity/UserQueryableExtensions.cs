using System.Linq;
using Enigmatry.Blueprint.Core.Helpers;

namespace Enigmatry.Blueprint.Model.Identity
{
    public static class UserQueryableExtensions
    {
        public static IQueryable<User> QueryByUserName(this IQueryable<User> query, string username)
        {
            return query.Where(e => e.UserName == username);
        }

        public static IQueryable<User> QueryByKeyword(this IQueryable<User> query, string keyword)
        {
            return keyword.HasContent() ? query.Where(e => e.UserName.Contains(keyword)) : query;
        }
    }
}
