using Enigmatry.Entry.Blueprint.Api.Features.Users;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.CodeGeneration.Validation;

namespace Enigmatry.Entry.Blueprint.Api.Features.Validations;

public class UserEditComponentValidationConfiguration : ValidationConfiguration<GetUserDetails.Response>
{
    public UserEditComponentValidationConfiguration()
    {
        RuleFor(x => x.FullName)
            .IsRequired()
            .MaxLength(User.NameMaxLength);
    }
}
