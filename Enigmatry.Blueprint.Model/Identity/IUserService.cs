using System.Collections.Generic;

namespace Enigmatry.Blueprint.Model.Identity
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
    }
}