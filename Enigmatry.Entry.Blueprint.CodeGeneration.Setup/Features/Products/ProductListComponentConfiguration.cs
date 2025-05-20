using System.Linq.Expressions;
using Enigmatry.Entry.Blueprint.Api.Features.Products;
using Enigmatry.Entry.CodeGeneration.Configuration.Formatters;
using Enigmatry.Entry.CodeGeneration.Configuration.List;
using Enigmatry.Entry.CodeGeneration.Configuration.List.Model;

namespace Enigmatry.Entry.Blueprint.CodeGeneration.Setup.Features.Products;

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
            .Column(x => x.Discount)
            .IsVisible(false);
        builder
            .Column(x => x.HasDiscount)
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
            .WithFormat(new CurrencyPropertyFormatter().WithCurrencyCode("EUR").WithDisplay("€"))
            .WithTranslationId(ProductEditComponentConfiguration.PriceLabel)
            .WithCustomCssClass("products-price");
        builder
            .Column(x => x.Amount)
            .WithHeaderName("Units")
            .WithTranslationId(ProductEditComponentConfiguration.AmountLabel);
        builder
            .Column(x => x.FreeShipping);

        DefineToggleColumn(builder, x => x.ContactEmail);
        DefineToggleColumn(builder, x => x.ContactPhone);
        DefineToggleColumn(builder, x => x.Code);
        DefineToggleColumn(builder, x => x.ExpiresOn, "hide-on-tablet");
        DefineToggleColumn(builder, x => x.FreeShipping);

        // Configuring list rows:
        builder
            .Row()
            .Selection(RowSelectionType.None)
            .ShowContextMenu(true); // Context menu items will be configured on client side dynamically
    }

    private static void DefineToggleColumn<T>(ListComponentBuilder<GetProducts.Response.Item> builder,
        Expression<Func<GetProducts.Response.Item, T>> definition, string className = "") =>
        builder
            .Column(definition)
            .WithCustomCssClass($"hide-on-mobile {className}");
}
