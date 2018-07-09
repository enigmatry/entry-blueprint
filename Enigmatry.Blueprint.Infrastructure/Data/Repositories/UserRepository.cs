using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;
using Enigmatry.Blueprint.Model.Identity;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Infrastructure.Data.Repositories
{
    public class UserRepository : EntityFrameworkRepository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
