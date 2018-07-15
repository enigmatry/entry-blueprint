using System.Linq;
using Enigmatry.Blueprint.Core.Data;
using FluentValidation;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Model.Identity
{
    [UsedImplicitly]
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        private readonly IRepository<User> _userRepository;

        public UserCreateDtoValidator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(50).EmailAddress();
            RuleFor(x => x.UserName).NotEmpty().Must(UniqueUsername).WithMessage("unique");
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }

        private bool UniqueUsername(string name)
        {
            return !_userRepository.QueryAll()
                .ByUserName(name)
                .Any();
        }
    }
}