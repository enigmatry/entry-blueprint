using System.Threading;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Core.Data;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Blueprint.Model.Identity.Commands
{
    [UsedImplicitly]
    public class UserCreateOrUpdateCommandHandler : IRequestHandler<UserCreateOrUpdate.Command, User>
    {
        private readonly IRepository<User> _userRepository;

        public UserCreateOrUpdateCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(UserCreateOrUpdate.Command request,
            CancellationToken cancellationToken)
        {
            User user;
            if (request.Id.HasValue)
            {
                user = await _userRepository.FindByIdAsync(request.Id.Value);
                user.Update(request);
            }
            else
            {
                user = User.Create(request);
                _userRepository.Add(user);
            }

            return user;
        }
    }
}
