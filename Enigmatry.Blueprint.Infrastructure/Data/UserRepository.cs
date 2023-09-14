using Enigmatry.Blueprint.Domain.Users;
using Enigmatry.Entry.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Infrastructure.Data;

public class UserRepository : EntityFrameworkRepository<User, Guid>
{
    public UserRepository(DbContext context) : base(context)
    {
    }
}
