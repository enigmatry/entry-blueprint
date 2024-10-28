using Enigmatry.Entry.Blueprint.Api.Features.Products;
using Enigmatry.Entry.Blueprint.Api.Features.Validations;
using Enigmatry.Entry.Blueprint.Domain.Products;
using Enigmatry.Entry.CodeGeneration.Configuration;
using Enigmatry.Entry.CodeGeneration.Configuration.Form;
using Enigmatry.Entry.CodeGeneration.Configuration.Form.Controls;
using Enigmatry.Entry.CodeGeneration.Configuration.Formatters;
using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.CodeGeneration.Setup.Features.Products;

[UsedImplicitly]
public class ProductEditComponentConfiguration : IFormComponentConfiguration<GetProductDetails.Response>
{
    public void Configure(FormComponentBuilder<GetProductDetails.Response> builder)
    {
        // Configuring component:
        builder.Component()
            .HasName("ProductEdit")
            .BelongsToFeature("Products")
            .OrderBy(OrderByType.Configuration);

        // Configuring fields:
        builder.FormControl(x => x.Name)
            .WithPlaceholder("Unique product name")
            .WithValidators("productNameIsUnique");

        builder.FormControl(x => x.Code)
            .WithPlaceholder("Unique product code identifier")
            .WithValidators("productCodeIsUnique");

        builder.AutocompleteFormControl(x => x.Type)
            .WithOptions(options => options.WithFixedValues<ProductType>().WithSortKey("displayName"));

        builder.RichTextInputFormControl(x => x.Description)
            .WithEditor(RichTextEditor.Redactor);

        builder.FormControl(x => x.Price)
            .WithLabel("Price per unit")
            .WithLabelTranslationId(ProductTranslationId.Price)
            .WithPlaceholder("Price per unit")
            .WithPlaceholderTranslationId(ProductTranslationId.Price)
            .WithFormat(new CurrencyPropertyFormatter().WithCurrencyCode("EUR").WithDisplay("€"));

        builder.FormControl(x => x.Amount)
            .WithLabel("Units")
            .WithLabelTranslationId(ProductTranslationId.Amount)
            .WithPlaceholder("Units")
            .WithPlaceholderTranslationId(ProductTranslationId.Amount);

        builder.FormControl(x => x.ContactEmail)
            .WithPlaceholder("Contact person email address");

        builder.FormControl(x => x.ContactPhone)
            .WithPlaceholder("Contact person phone number");

        builder.FormControl(x => x.InfoLink)
            .WithLabel("Homepage")
            .WithPlaceholder("Link to product homepage");

        builder.FormControl(x => x.AdditionalInfo).Ignore();

        builder.FormControl(x => x.ExpiresOn)
            .WithPlaceholder("Product expiration date, if any");

        builder.FormControl(x => x.FreeShipping);

        builder.FormControl(x => x.HasDiscount);

        builder.FormControl(x => x.Discount)
            .WithFormat(new PercentPropertyFormatter().WithDigitsInfo("1.2-2"));

        builder.ButtonFormControl("ResetFormBtn")
            .WithLabel(String.Empty)
            .WithText("Reset")
            .WithHint("* This will reset form to it's initial state.");

        // Configuring built in validations
        builder.WithValidationConfiguration(new ProductEditComponentValidationConfiguration());
    }
}
