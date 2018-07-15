using System;
using System.Linq;
using System.Security.Principal;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Infrastructure.Identity
{
    [UsedImplicitly]
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly Func<IPrincipal> _principalProvider;
        private readonly IQueryable<User> _userQuery;
        private User _user;

        public CurrentUserProvider(Func<IPrincipal> principalProvider,
            IQueryable<User> userQuery)
        {
            _principalProvider = principalProvider;
            _userQuery = userQuery;
        }

        private IPrincipal Principal => _principalProvider();

        public bool IsAuthenticated => Principal.Identity.IsAuthenticated;

        public int UserId => User.Id;

        public User User
        {
            get
            {
                if (_user != null)
                {
                    return _user;
                }

                _user = _userQuery
                    .ByUserName(Principal.Identity.Name)
                    .SingleOrDefault();

                return _user;
            }
        }
    }
}