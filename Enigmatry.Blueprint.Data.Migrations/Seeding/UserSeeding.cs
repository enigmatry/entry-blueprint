using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding
{
    public class UserSeeding : ISeeding
    {
        public void Seed(BlueprintContext context)
        {
            /*var user = context.Set<User>().FirstOrDefault(u => u.Name == "Test");
            if (user == null)
            {
                user = User.Create("Test", "Test", DateTimeOffset.Now);

                context.Set<User>().Add(user);
            }*/
        }
    }
}
