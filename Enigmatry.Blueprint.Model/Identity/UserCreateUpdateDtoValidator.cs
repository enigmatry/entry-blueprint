using System.Linq;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Core.Data;
using FluentValidation;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Model.Identity
{
    [UsedImplicitly]
    public class UserCreateUpdateDtoValidator : AbstractValidator<UserCreateUpdateDto>
    {
        private readonly IRepository<User> _userRepository;

        public UserCreateUpdateDtoValidator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(50).EmailAddress();
            RuleFor(x => x.UserName).Must(UniqueUsername).WithMessage("unique");
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }

        private bool UniqueUsername(UserCreateUpdateDto dto, string name)
        {
            return !_userRepository.QueryAll()
                .ExceptWithId(dto.Id)
                .ByUserName(name)
                .Any();
        }
    }
}