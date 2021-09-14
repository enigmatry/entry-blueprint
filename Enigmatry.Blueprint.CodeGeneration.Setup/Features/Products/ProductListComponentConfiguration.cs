using Enigmatry.Blueprint.Api.Features.Products;
using Enigmatry.CodeGeneration.Configuration.List;
using Enigmatry.CodeGeneration.Configuration.List.Model;

namespace Enigmatry.Blueprint.CodeGeneration.Setup.Features.Products
{
    public class ProductListComponentConfiguration : IListComponentConfiguration<GetProducts.Response.Item>
    {
        public void Configure(ListComponentBuilder<GetProducts.Response.Item> builder)
        {
            builder.Component()
                .HasName("ProductList")
                .BelongsToFeature("Products");


            builder.Row().Selection(RowSelectionType.Single);

            builder.Pagination().ShowFirstLastPageButtons(false);

            builder.Row()
                .ShowContextMenu(true)
                .ContextMenuItems(new RowContextMenuItem { Id = "edit", Name = "Edit", Icon = "edit" });
        }
    }
}
