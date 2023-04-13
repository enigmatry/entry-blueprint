using Enigmatry.Blueprint.Api.Features.Users;
using Enigmatry.Blueprint.Api.Features.Validations;
using Enigmatry.Entry.CodeGeneration.Configuration.Form;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.CodeGeneration.Setup.Features.Users;

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

        userSection.InputFormControl(x => x.UserName).IsReadonly(true);
        userSection.InputFormControl(x => x.Name);

        var historySection = userSection.FormControlGroup("History");

        historySection.DatepickerFormControl(x => x.CreatedOn).IsReadonly(true);
        historySection.DatepickerFormControl(x => x.UpdatedOn).IsReadonly(true);


        builder.WithValidationConfiguration(new UserEditComponentValidationConfiguration());
    }
}
