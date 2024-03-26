using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Core.Data;

namespace Enigmatry.Entry.Blueprint.Scheduler;

public class SchedulerUserProvider : ICurrentUserProvider
{
    private readonly IRepository<User> _userRepository;

    public SchedulerUserProvider(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public User? User => _userRepository.FindById(User.SchedulerUserId) ?? null;
    public bool IsAuthenticated => true;
}
