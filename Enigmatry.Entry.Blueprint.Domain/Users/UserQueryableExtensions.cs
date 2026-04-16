using Enigmatry.Entry.Core.Helpers;

namespace Enigmatry.Entry.Blueprint.Domain.Users;

public static class UserQueryableExtensions
{
    extension(IQueryable<User> query)
    {
        public IQueryable<User> QueryByEmailAddress(string emailAddress) => query.Where(e => e.EmailAddress == emailAddress);

        public IQueryable<User> QueryByKeyword(string keyword) =>
            keyword.HasContent()
                ? query.Where(e => e.EmailAddress.Contains(keyword))
                : query;
    }
}
