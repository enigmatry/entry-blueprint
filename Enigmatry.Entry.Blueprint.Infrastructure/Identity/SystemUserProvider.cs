using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Scheduler;

public class SystemUserProvider : ICurrentUserProvider
{
    private readonly IRepository<User> _userRepository;
    private User? _user;

    public SystemUserProvider(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public User? User
    {
        get
        {
            if (_user != null)
            {
                return _user;
            }

            _user = _userRepository
                .QueryAll()
                .QueryById(User.SystemUserId)
                .Include(u => u.Role)
                .ThenInclude(r => r.Permissions)
                .AsNoTracking()
                .AsSplitQuery()
                .SingleOrDefault();

            return _user;
        }
    }

    public bool IsAuthenticated => true;
}
