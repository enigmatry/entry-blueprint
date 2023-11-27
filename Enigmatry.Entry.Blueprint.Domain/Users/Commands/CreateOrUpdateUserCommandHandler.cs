using Enigmatry.Entry.Core.Data;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Entry.Blueprint.Domain.Users.Commands;

[UsedImplicitly]
public class CreateOrUpdateUserCommandHandler : IRequestHandler<CreateOrUpdateUser.Command, User>
{
    private readonly IRepository<User, Guid> _userRepository;

    public CreateOrUpdateUserCommandHandler(IRepository<User, Guid> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(CreateOrUpdateUser.Command request,
        CancellationToken cancellationToken)
    {
        User? user;
        if (request.Id.HasValue)
        {
            user = await _userRepository.FindByIdAsync(request.Id.Value);
            if (user == null)
            {
                throw new InvalidOperationException("missing user");
            }
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
