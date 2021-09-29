using Enigmatry.Blueprint.Api.Features.Products;
using Enigmatry.CodeGeneration.Configuration.List;
using Enigmatry.CodeGeneration.Configuration.List.Model;

namespace Enigmatry.Blueprint.CodeGeneration.Setup.Features.Products
{
    public class ProductListComponentConfiguration : IListComponentConfiguration<GetProducts.Response.Item>
    {
        public void Configure(ListComponentBuilder<GetProducts.Response.Item> builder)
        {
            // Configuring component:
            builder
                .Component()
                .HasName("ProductList")
                .BelongsToFeature("Products");

            // Configuring list columns:
            builder
                .Column(x => x.InfoLink)
                .IsVisible(false);
            builder
                .Column(x => x.Name)
                .WithHeaderName("Product name")
                .WithCustomComponent("app-product-name-with-link-cell");
            builder
                .Column(x => x.Type)
                .WithCustomComponent("app-product-type-cell");
            builder
                .Column(x => x.Price)
                .WithHeaderName("Price per unit")
                .WithTranslationId(ProductTranslationId.Price)
                .WithCustomCssClass("products-price");
            builder
                .Column(x => x.Amount)
                .WithHeaderName("Units")
                .WithTranslationId(ProductTranslationId.Amount);

            // Configuring list rows:
            builder
                .Row()
                .Selection(RowSelectionType.None)
                .ShowContextMenu(true); // Context menu items will be configured on client side dynamically

            // Configuring list pagination:
            builder
                .Pagination()
                .PageSizeOptions(new[] { 2, 5, 10, 25, 50 });
        }
    }
}
