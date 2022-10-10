using Enigmatry.Blueprint.Api.Features.Products;
using Enigmatry.Blueprint.Api.Features.Validations;
using Enigmatry.Blueprint.Domain.Products;
using Enigmatry.CodeGeneration.Configuration;
using Enigmatry.CodeGeneration.Configuration.Form;
using Enigmatry.CodeGeneration.Configuration.Form.Controls;
using Enigmatry.CodeGeneration.Configuration.Formatters;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.CodeGeneration.Setup.Features.Products;

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
            .WithAppearance(FormControlAppearance.Outline)
            .WithValidators("productNameIsUnique");

        builder.FormControl(x => x.Code)
            .WithPlaceholder("Unique product code identifier")
            .WithAppearance(FormControlAppearance.Outline)
            .WithValidators("productCodeIsUnique");

        builder.AutocompleteFormControl(x => x.Type)
            .WithAppearance(FormControlAppearance.Outline)
            .WithOptions(options => options.WithFixedValues<ProductType>().WithSortKey("displayName"));

        builder.TextareaFormControl(x => x.Description)
            .WithAppearance(FormControlAppearance.Outline)
            .WithRows(2);

        builder.FormControl(x => x.Price)
            .WithLabel("Price per unit")
            .WithLabelTranslationId(ProductTranslationId.Price)
            .WithPlaceholder("Price per unit")
            .WithPlaceholderTranslationId(ProductTranslationId.Price)
            .WithAppearance(FormControlAppearance.Outline)
            .WithFormat(new CurrencyPropertyFormatter().WithCurrencyCode("EUR").WithDisplay("€"));

        builder.FormControl(x => x.Amount)
            .WithLabel("Units")
            .WithAppearance(FormControlAppearance.Outline)
            .WithLabelTranslationId(ProductTranslationId.Amount)
            .WithPlaceholder("Units")
            .WithPlaceholderTranslationId(ProductTranslationId.Amount);

        builder.FormControl(x => x.ContactEmail)
            .WithAppearance(FormControlAppearance.Outline)
            .WithPlaceholder("Contact person email address");

        builder.FormControl(x => x.ContactPhone)
            .WithAppearance(FormControlAppearance.Outline)
            .WithPlaceholder("Contact person phone number");

        builder.FormControl(x => x.InfoLink)
            .WithAppearance(FormControlAppearance.Outline)
            .WithLabel("Homepage")
            .WithPlaceholder("Link to product homepage");

        builder.FormControl(x => x.AdditionalInfo).Ignore();

        builder.FormControl(x => x.ExpiresOn)
            .WithAppearance(FormControlAppearance.Outline)
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
