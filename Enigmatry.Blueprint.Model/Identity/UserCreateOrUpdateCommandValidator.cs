using System.Linq;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Core.Data;
using FluentValidation;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Model.Identity
{
    [UsedImplicitly]
    public class UserCreateOrUpdateCommandValidator : AbstractValidator<UserCreateOrUpdateCommand>
    {
        private readonly IRepository<User> _userRepository;

        public UserCreateOrUpdateCommandValidator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(50).EmailAddress();
            RuleFor(x => x.UserName).Must(UniqueUsername).WithMessage("unique");
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }

        private bool UniqueUsername(UserCreateOrUpdateCommand dto, string name)
        {
            return !_userRepository.QueryAll()
                .ExceptWithId(dto.Id)
                .ByUserName(name)
                .Any();
        }
    }
}