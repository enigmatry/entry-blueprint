using Enigmatry.Blueprint.Api.Features.Users;
using Enigmatry.CodeGeneration.Configuration.Form;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.CodeGeneration.Setup.Features.Users
{
    [UsedImplicitly]
    public class UserEditComponentConfiguration : IFormComponentConfiguration<GetUserDetails.Response>
    {
        public void Configure(FormComponentBuilder<GetUserDetails.Response> builder)
        {
            builder.Component()
                .HasName("UserEdit")
                .BelongsToFeature("Users");

            builder.FormControl(x => x.UserName).IsReadonly(true);
            builder.FormControl(x => x.CreatedOn).IsReadonly(true);
            builder.FormControl(x => x.UpdatedOn).IsReadonly(true);

            builder.WithValidationConfiguration(new UserEditValidationConfiguration());
        }
    }
}
