using Enigmatry.Blueprint.Api.Features.Users;
using Enigmatry.Blueprint.Model.Identity;
using Enigmatry.BuildingBlocks.Validation;

namespace Enigmatry.Blueprint.Api.Features.Validations
{
    public class UserEditComponentValidationConfiguration : ValidationConfiguration<GetUserDetails.Response>
    {
        public UserEditComponentValidationConfiguration()
        {
            RuleFor(x => x.Name)
                .IsRequired()
                .MinLength(User.NameMinLenght)
                .MaxLength(User.NameMaxLenght);
        }
    }
}
