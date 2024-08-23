using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Data;

public class UserRepository(DbContext context) : EntityFrameworkRepository<User, Guid>(context);
