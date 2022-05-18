using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.BuildingBlocks.Core.Data;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.ApplicationServices.Identity;

[UsedImplicitly]
public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly ICurrentUserIdProvider _currentUserIdProvider;
    private readonly IRepository<User> _userRepository;
    private User? _user;

    public CurrentUserProvider(ICurrentUserIdProvider currentUserIdProvider,
        IRepository<User> userRepository)
    {
        _userRepository = userRepository;
        _currentUserIdProvider = currentUserIdProvider;
    }

    public bool IsAuthenticated => _currentUserIdProvider.IsAuthenticated;

    public Guid? UserId => _currentUserIdProvider.UserId.GetValueOrDefault();

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
                return null;
            }

            //TODO replace with getting user from principal
            _user = _userRepository.QueryAll()
                .First();

            // e.g. 
            /*_user = _userQuery
                .ByUserName(Principal.Identity.Name)
                .SingleOrDefault();*/

            return _user;
        }
    }
}
