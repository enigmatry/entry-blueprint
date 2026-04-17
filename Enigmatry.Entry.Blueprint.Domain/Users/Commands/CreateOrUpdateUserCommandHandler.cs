using Enigmatry.Entry.Core.Data;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Entry.Blueprint.Domain.Users.Commands;

[UsedImplicitly]
public class CreateOrUpdateUserCommandHandler(IRepository<User, Guid> userRepository) : IRequestHandler<CreateOrUpdateUser.Command, User>
{
    public async Task<User> Handle(CreateOrUpdateUser.Command request,
        CancellationToken cancellationToken)
    {
        User? user;
        if (request.Id.HasValue)
        {
            user = await userRepository.FindByIdAsync(request.Id.Value)
                ?? throw new InvalidOperationException("missing user");
            user.Update(request);
        }
        else
        {
            user = User.Create(request);
            userRepository.Add(user);
        }

        return user;
    }
}
