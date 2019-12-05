using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Core.Email;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Blueprint.ApplicationServices.Identity
{
    [UsedImplicitly]
    public class UserCreateOrUpdateCommandHandler : IRequestHandler<UserCreateOrUpdateCommand, User>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IEmailClient _emailClient;

        public UserCreateOrUpdateCommandHandler(IRepository<User> userRepository, IEmailClient emailClient)
        {
            _userRepository = userRepository;
            _emailClient = emailClient;
        }

        public async Task<User> Handle(UserCreateOrUpdateCommand request,
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

                _emailClient.Send(new EmailMessage("New user", "Your account is successfully created.", new[] {user.UserName}, Enumerable.Empty<string>()));
            }

            return user;
        }
    }
}
