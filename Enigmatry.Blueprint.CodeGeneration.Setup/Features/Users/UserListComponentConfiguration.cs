using Enigmatry.Blueprint.Api.Features.Users;
using Enigmatry.Entry.CodeGeneration.Configuration.List;
using Enigmatry.Entry.CodeGeneration.Configuration.List.Model;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.CodeGeneration.Setup.Features.Users;

[UsedImplicitly]
public class UserListComponentConfiguration : IListComponentConfiguration<GetUsers.Response.Item>
{
    public void Configure(ListComponentBuilder<GetUsers.Response.Item> builder)
    {
        // Configure component:
        // - component name and feature name:

        builder.Component()
            .HasName("UserList")
            .BelongsToFeature("Users");

        // Configure columns:
        // - HeaderName                 
        // - IsVisible                  (default true except for 'Id' column)
        // - IsSortable                 (default true)
        // - TranslationId
        // - Formatter

        builder.Column(x => x.EmailAddress)
            .WithHeaderName("E-mail");

        // Configure selection:
        // None, Single or Multiple     (default None)

        builder.Row().Selection(RowSelectionType.Single);

        // Configure pagination: 
        // - ShowPaginator              (default true)
        // - PageSizeOptions            (default [10, 50, 100])
        // - ShowPageSize               (default true)
        // - ShowFirstLastPageButtons   (default true)

        builder.Pagination().ShowFirstLastPageButtons(false);

        // Configure context menu:
        // - ShowContextMenu            (default false)
        // - ContextMenuItems:
        // as string values or RowContextMenuItem values

        builder.Row()
            .Selection(RowSelectionType.Multiple, false)
            .ShowContextMenu(true); // Context menu items will be configured on the frontend
    }
}
