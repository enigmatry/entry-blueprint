using Enigmatry.Entry.Blueprint.Api.Features.Users;
using Enigmatry.Entry.Blueprint.Api.Features.Validations;
using Enigmatry.Entry.CodeGeneration.Configuration.Form;
using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.CodeGeneration.Setup.Features.Users;

[UsedImplicitly]
public class UserEditComponentConfiguration : IFormComponentConfiguration<GetUserDetails.Response>
{
    internal const string UserLabel = "users.user-edit.user.label";
    internal const string EmailLabel = "users.user-edit.email-address.label";
    internal const string FullNameLabel = "users.user-edit.full-name.label";
    internal const string RoleLabel = "users.user-edit.role-id.label";
    internal const string HistoryLabel = "users.user-edit.history.label";
    internal const string StatusLabel = "users.user-edit.user-status-id.label";
    internal const string CreatedOnLabel = "users.user-edit.created-on.label";
    internal const string UpdatedOnLabel = "users.user-edit.updated-on.label";

    public void Configure(FormComponentBuilder<GetUserDetails.Response> builder)
    {
        builder.Component()
            .HasName("UserEdit")
            .BelongsToFeature("Users")
            .IncludeUnconfiguredProperties(false);

        var userSection = builder.FormControlGroup("User")
            .WithTranslationId(UserLabel);

        userSection.FormControl(x => x.EmailAddress).WithTranslationId(EmailLabel).IsReadonly(true);

        userSection.FormControl(x => x.FullName).WithTranslationId(FullNameLabel);

        userSection.SelectFormControl(x => x.RoleId)
            .WithOptions(options => options.WithDynamicValues())
            .WithLabel("Role").WithTranslationId(RoleLabel);

        userSection.SelectFormControl(x => x.UserStatusId)
            .WithOptions(options => options.WithDynamicValues())
            .WithLabel("Status")
            .WithTranslationId(StatusLabel);

        var historySection = userSection.FormControlGroup("History")
            .WithTranslationId(HistoryLabel);

        historySection.FormControl(x => x.CreatedOn).IsReadonly(true).WithTranslationId(CreatedOnLabel);
        historySection.FormControl(x => x.UpdatedOn).IsReadonly(true).WithTranslationId(UpdatedOnLabel);

        builder.WithValidationConfiguration(new UserEditComponentValidationConfiguration());
    }
}
