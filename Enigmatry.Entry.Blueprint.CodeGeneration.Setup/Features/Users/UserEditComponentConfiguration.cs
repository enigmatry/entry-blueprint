using Enigmatry.Entry.Blueprint.Api.Features.Users;
using Enigmatry.Entry.Blueprint.Api.Features.Validations;
using Enigmatry.Entry.CodeGeneration.Configuration.Form;
using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.CodeGeneration.Setup.Features.Users;

[UsedImplicitly]
public class UserEditComponentConfiguration : IFormComponentConfiguration<GetUserDetails.Response>
{
    public void Configure(FormComponentBuilder<GetUserDetails.Response> builder)
    {
        builder.Component()
            .HasName("UserEdit")
            .BelongsToFeature("Users")
            .IncludeUnconfiguredProperties(false);

        var userSection = builder.FormControlGroup("User");

        userSection.FormControl(x => x.EmailAddress).IsReadonly(true);
        userSection.FormControl(x => x.FullName);

        userSection.SelectFormControl(x => x.RoleId)
            .WithOptions(options => options.WithDynamicValues())
            .WithLabel("Role");

        var historySection = userSection.FormControlGroup("History");

        historySection.FormControl(x => x.CreatedOn).IsReadonly(true);
        historySection.FormControl(x => x.UpdatedOn).IsReadonly(true);


        builder.WithValidationConfiguration(new UserEditComponentValidationConfiguration());
    }
}
