﻿using Enigmatry.Blueprint.Api.Features.Users;
using Enigmatry.Blueprint.Api.Features.Validations;
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

            var userSection = builder.FormControlGroup("User")
                .CreateUiSection("fieldset");

            userSection.FormControl(x => x.UserName).IsReadonly(true);
            userSection.FormControl(x => x.Name);

            var historySection = userSection.FormControlGroup("History")
                .CreateUiSection("fieldset");

            historySection.FormControl(x => x.CreatedOn).IsReadonly(true);
            historySection.FormControl(x => x.UpdatedOn).IsReadonly(true);


            builder.WithValidationConfiguration(new UserEditComponentValidationConfiguration());
        }
    }
}
