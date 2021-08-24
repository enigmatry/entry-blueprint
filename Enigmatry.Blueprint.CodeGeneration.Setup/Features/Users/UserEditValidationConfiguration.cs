using Enigmatry.Blueprint.Api.Features.Users;
using Enigmatry.BuildingBlocks.Validation;

namespace Enigmatry.Blueprint.CodeGeneration.Setup.Features.Users
{
    public class UserEditValidationConfiguration : ValidationConfiguration<GetUserDetails.Response>
    {
        public UserEditValidationConfiguration()
        {
            RuleFor(x => x.Name)
                .IsRequired("Name is required")
                .HasMinLength(5, "Name min length is 5")
                .HasMaxLength(25);

            RuleFor(x => x.Age)
                .IsRequired("Age is required")
                .HasMinLength(1)
                .HasMaxLength(100, "Max age is 100");

            RuleFor(x => x.Email)
                .IsRequired()
                .IsEmail("Must be in email format (e.g. john.doe@google.com)");
        }
    }
}
