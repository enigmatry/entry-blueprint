using System.Linq;

namespace Enigmatry.Blueprint.Model.Identity
{
    public static class UserQueryableExtensions
    {
        public static IQueryable<User> QueryByUserName(this IQueryable<User> query, string username)
        {
            return query.Where(e => e.UserName == username);
        }
    }
}
