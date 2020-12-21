using System;
using Enigmatry.Blueprint.BuildingBlocks.EntityFramework;
using Enigmatry.Blueprint.Model.Identity;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Infrastructure.Data
{
    public class UserRepository : EntityFrameworkRepository<User, Guid>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
