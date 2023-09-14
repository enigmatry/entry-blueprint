using Enigmatry.Blueprint.Api.Features.Users;
using Enigmatry.Blueprint.Domain.Users;
using Enigmatry.Entry.CodeGeneration.Validation;

namespace Enigmatry.Blueprint.Api.Features.Validations;

public class UserEditComponentValidationConfiguration : ValidationConfiguration<GetUserDetails.Response>
{
    public UserEditComponentValidationConfiguration()
    {
        RuleFor(x => x.FullName)
            .IsRequired()
            .MaxLength(User.NameMaxLength);
    }
}
