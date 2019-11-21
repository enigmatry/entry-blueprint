using System;
using System.Security.Principal;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.ApplicationServices.Identity
{
    [UsedImplicitly]
    public class CurrentUserIdProvider : ICurrentUserIdProvider
    {
        private readonly Func<IPrincipal> _principalProvider;

        public CurrentUserIdProvider(Func<IPrincipal> principalProvider) => _principalProvider = principalProvider;

        private IPrincipal Principal => _principalProvider();

        public bool IsAuthenticated => Principal.Identity.IsAuthenticated;

        public Guid? UserId
        {
            get
            {
                if (!IsAuthenticated)
                {
                    return null;
                }

                //TODO replace with getting userId from principal
                Guid? userId = null;

                return userId;
            }
        }
    }
}
