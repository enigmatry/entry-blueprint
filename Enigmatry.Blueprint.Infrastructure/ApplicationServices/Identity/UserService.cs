using System.Collections.Generic;
using System.Linq;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Infrastructure.ApplicationServices.Identity
{
    [UsedImplicitly]
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.QueryAll().ToList();
        }
    }
}