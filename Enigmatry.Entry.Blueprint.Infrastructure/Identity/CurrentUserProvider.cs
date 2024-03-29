using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Identity;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IClaimsProvider _claimsProvider;
    private readonly IRepository<User> _userRepository;
    private readonly ILogger<CurrentUserProvider> _logger;
    private User? _user;

    public CurrentUserProvider(IClaimsProvider claimsProvider,
        IRepository<User> userRepository,
        ILogger<CurrentUserProvider> logger)
    {
        _claimsProvider = claimsProvider;
        _userRepository = userRepository;
        _logger = logger;
    }

    public bool IsAuthenticated => _claimsProvider.IsAuthenticated;

    public User? User
    {
        get
        {
            if (_user != null)
            {
                return _user;
            }

            if (!IsAuthenticated)
            {
                _logger.LogWarning("User is not authenticated");
                return null;
            }

            if (String.IsNullOrEmpty(_claimsProvider.Email))
            {
                _logger.LogWarning("User's email was not found in the claims");
                return null;
            }

            _user = _userRepository
                .QueryAll()
                .QueryByEmailAddress(_claimsProvider.Email)
                .Include(u => u.Role)
                .ThenInclude(r => r.Permissions)
                .AsNoTracking()
                .AsSplitQuery()
                .SingleOrDefault();

            return _user;
        }
    }
}
