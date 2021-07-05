using Enigmatry.Blueprint.Model.Identity.Commands;
using Enigmatry.CodeGeneration.Configuration.Form;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.CodeGeneration.Setup.Features.Users
{
    [UsedImplicitly]
    internal class UserEditComponentConfiguration : IFormComponentConfiguration<UserCreateOrUpdate.Command>
    {
        public void Configure(FormComponentBuilder<UserCreateOrUpdate.Command> builder)
        {
            builder.Component()
                .HasName("UserEdit")
                .BelongsToFeature("Users");

            builder.HasCreateOrUpdateCommandWithName(nameof(UserCreateOrUpdate.Command));

            builder.FormControl(x => x.Id).IsVisible(false);

            builder.FormControl(x => x.UserName)
                .WithLabel("UserName")
                .WithPlaceholder("UserName");

            builder.FormControl(x => x.Name)
                .WithLabel("Name")
                .WithPlaceholder("Name");
        }
    }
}
