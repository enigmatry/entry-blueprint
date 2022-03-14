using System.ComponentModel;
using Enigmatry.BuildingBlocks.Core.Data;
using Enigmatry.BuildingBlocks.Core.Entities;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Blueprint.Model.Identity.Commands;

public static class UserCreateOrUpdate
{
    [PublicAPI]
    public class Command : IRequest<User>
    {
        public Guid? Id { get; set; }
        [DisplayName("Username")]
        public string UserName { get; set; } = "";
        [DisplayName("Name")]
        public string Name { get; set; } = "";
    }

    [UsedImplicitly]
    public class Validator : AbstractValidator<Command>
    {
        private readonly IRepository<User> _userRepository;

        public Validator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            _ = RuleFor(x => x.UserName).NotEmpty().MaximumLength(50).EmailAddress();
            _ = RuleFor(x => x.UserName).Must(UniqueUsername).WithMessage("Username already taken");
            _ = RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }

        private bool UniqueUsername(Command command, string name) =>
            !_userRepository.QueryAll()
                .QueryExceptWithId(command.Id)
                .QueryByUserName(name)
                .Any();
    }
}
