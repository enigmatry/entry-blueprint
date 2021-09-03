using Enigmatry.Blueprint.Api.Features.Users;
using Enigmatry.BuildingBlocks.Validation;

namespace Enigmatry.Blueprint.CodeGeneration.Setup.Features.Users
{
    public class UserEditValidationConfiguration : AbstractValidationConfiguration<GetUserDetails.Response>
    {
        public UserEditValidationConfiguration()
        {
            RuleFor(x => x.Name)
                .IsRequired()
                    .WithMessage("Name is required")
                .Min(5)
                    .WithMessage("Name min length is 5")
                .Max(25);

            RuleFor(x => x.Age)
                .IsRequired()
                    .WithMessage("Age is required")
                .Min(1)
                .Max(100)
                    .WithMessage("Max age is 100");

            RuleFor(x => x.Email)
                .IsRequired()
                .IsEmailAddress()
                    .WithMessage("Must be in email format (e.g. john.doe@google.com)");
        }
    }
}
