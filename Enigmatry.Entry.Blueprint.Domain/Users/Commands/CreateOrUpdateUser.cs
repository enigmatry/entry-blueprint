using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.Entities;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Entry.Blueprint.Domain.Users.Commands;

public static class CreateOrUpdateUser
{
    [PublicAPI]
    public class Command : IRequest<User>
    {
        public required Guid? Id { get; set; }
        public required string EmailAddress { get; set; } = String.Empty;
        public required string FullName { get; set; } = String.Empty;
        public required Guid RoleId { get; set; }
        public UserStatusId UserStatusId { get; set; } = UserStatusId.Active;
    }

    [UsedImplicitly]
    public class Validator : AbstractValidator<Command>
    {
        private readonly IRepository<User> _userRepository;

        public Validator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.EmailAddress).NotEmpty().MaximumLength(User.EmailAddressMaxLength).EmailAddress();
            RuleFor(x => x.EmailAddress).Must(UniqueEmailAddress).WithMessage("EmailAddress is already taken");
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(User.NameMaxLength);
            RuleFor(x => x.RoleId).NotEmpty();
        }

        private bool UniqueEmailAddress(Command command, string name) =>
            !_userRepository.QueryAll()
                .QueryExceptWithId(command.Id)
                .QueryByEmailAddress(name)
                .Any();
    }
}
