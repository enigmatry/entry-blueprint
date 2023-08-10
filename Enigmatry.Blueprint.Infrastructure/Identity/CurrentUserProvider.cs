using System.Security.Principal;
using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Core.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Infrastructure.Identity;

[UsedImplicitly]
public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly Func<IPrincipal> _principalProvider;
    private readonly IRepository<User> _userRepository;
    private User? _user;

    public CurrentUserProvider(Func<IPrincipal> principalProvider,
        IRepository<User> userRepository)
    {
        _principalProvider = principalProvider;
        _userRepository = userRepository;
    }

    private IPrincipal Principal => _principalProvider();

    private string? Email => IsAuthenticated ? Principal.Identity!.Name : null;

    public bool IsAuthenticated => (Principal.Identity?.IsAuthenticated).GetValueOrDefault();

    public User? User
    {
        get
        {
            if (_user != null)
            {
                return _user;
            }

            if (!IsAuthenticated || String.IsNullOrEmpty(Email))
            {
                return null;
            }

            _user = _userRepository
                .QueryAll()
                .QueryByUserName(Email)
                .Include(u => u.Role)
                .ThenInclude(r => r.Permissions)
                .AsNoTracking().AsSplitQuery()
                .SingleOrDefault();

            return _user;
        }
    }
}
