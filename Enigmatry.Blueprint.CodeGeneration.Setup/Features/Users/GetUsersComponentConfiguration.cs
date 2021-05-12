using Enigmatry.Blueprint.Api.Features.Users;
using Enigmatry.CodeGeneration.Configuration.List;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.CodeGeneration.Setup.Features.Users
{
    [UsedImplicitly]
    public class GetUsersComponentConfiguration : IListComponentConfiguration<GetUsers.Response.Item>
    {
        public void Configure(ListComponentBuilder<GetUsers.Response.Item> builder)
        {
            builder.Component()
                .HasName("UserList")
                .BelongsToFeature("Users");
        }
    }
}
