using Enigmatry.Blueprint.Api.Features.Products;
using Enigmatry.Blueprint.Api.Features.Validations;
using Enigmatry.Blueprint.Domain.Products;
using Enigmatry.Entry.CodeGeneration.Configuration;
using Enigmatry.Entry.CodeGeneration.Configuration.Form;
using Enigmatry.Entry.CodeGeneration.Configuration.Form.Controls;
using Enigmatry.Entry.CodeGeneration.Configuration.Formatters;
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
        builder.InputFormControl(x => x.Name)
            .WithPlaceholder("Unique product name")
            .WithAppearance(FormControlAppearance.Outline)
            .WithValidators("productNameIsUnique");

        builder.InputFormControl(x => x.Code)
            .WithPlaceholder("Unique product code identifier")
            .WithAppearance(FormControlAppearance.Outline)
            .WithValidators("productCodeIsUnique");

        builder.AutocompleteFormControl(x => x.Type)
            .WithAppearance(FormControlAppearance.Outline)
            .WithOptions(options => options.WithFixedValues<ProductType>().WithSortKey("displayName"));

        builder.TextareaFormControl(x => x.Description)
            .WithAppearance(FormControlAppearance.Outline)
            .WithRows(2);

        builder.InputFormControl(x => x.Price)
            .WithLabel("Price per unit")
            .WithLabelTranslationId(ProductTranslationId.Price)
            .WithPlaceholder("Price per unit")
            .WithPlaceholderTranslationId(ProductTranslationId.Price)
            .WithAppearance(FormControlAppearance.Outline)
            .WithFormat(new CurrencyPropertyFormatter().WithCurrencyCode("EUR").WithDisplay("€"));

        builder.InputFormControl(x => x.Amount)
            .WithLabel("Units")
            .WithAppearance(FormControlAppearance.Outline)
            .WithLabelTranslationId(ProductTranslationId.Amount)
            .WithPlaceholder("Units")
            .WithDefaultValue("1")
            .WithPlaceholderTranslationId(ProductTranslationId.Amount);

        builder.EmailFormControl(x => x.ContactEmail)
            .WithAppearance(FormControlAppearance.Outline)
            .WithPlaceholder("Contact person email address");

        builder.InputFormControl(x => x.ContactPhone)
            .WithAppearance(FormControlAppearance.Outline)
            .WithPlaceholder("Contact person phone number");

        builder.InputFormControl(x => x.InfoLink)
            .WithAppearance(FormControlAppearance.Outline)
            .WithLabel("Homepage")
            .WithPlaceholder("Link to product homepage");

        builder.InputFormControl(x => x.AdditionalInfo).Ignore();

        builder.DatepickerFormControl(x => x.ExpiresOn)
            .WithAppearance(FormControlAppearance.Outline)
            .WithPlaceholder("Product expiration date, if any");

        builder.CheckboxFormControl(x => x.FreeShipping)
            .WithDefaultValue(true);

        builder.CheckboxFormControl(x => x.HasDiscount);

        builder.InputFormControl(x => x.Discount)
            .WithFormat(new PercentPropertyFormatter()
            .WithDigitsInfo("1.2-2"));

        builder.DatepickerFormControl(x => x.DiscountExpiresOn)
            .WithAppearance(FormControlAppearance.Outline)
            .WithDefaultValue(new DateTimeOffset(2022, 1, 1, 12, 0, 0, TimeSpan.Zero));

        builder.ButtonFormControl("ResetFormBtn")
            .WithLabel(String.Empty)
            .WithText("Reset")
            .WithHint("* This will reset form to it\\'s initial state.");

        // Configuring built in validations
        builder.WithValidationConfiguration(new ProductEditComponentValidationConfiguration());
    }
}
