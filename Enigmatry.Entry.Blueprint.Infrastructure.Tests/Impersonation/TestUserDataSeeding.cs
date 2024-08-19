using Enigmatry.Entry.Blueprint.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Tests.Impersonation;

public class TestUserDataSeeding(DbContext context)
{
    public void Seed()
    {
        RecreateUser(TestUserData.CreateTestUser());
        RecreateUser(TestUserData.CreateSystemUser());
        context.SaveChanges();
    }

    private void RecreateUser(User user)
    {
        var usersSet = context.Set<User>();
        var fromDb = usersSet.Find(user.Id);
        if (fromDb == null)
        {
            usersSet.Add(user);
        }
    }
}
