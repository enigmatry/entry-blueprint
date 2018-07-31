using System.Threading;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Infrastructure.ApplicationServices.Identity
{
    [UsedImplicitly]
    public class UserCreateOrUpdateCommandHandler : IRequestHandler<UserCreateOrUpdateCommand, User>
    {
        private readonly IRepository<User> _userRepository;

        public UserCreateOrUpdateCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(UserCreateOrUpdateCommand request,
            CancellationToken cancellationToken)
        {
            User user;
            if (request.Id.HasValue)
            {
                user = await _userRepository.QueryAll()
                    .ById(request.Id.Value)
                    .SingleAsync(cancellationToken);

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