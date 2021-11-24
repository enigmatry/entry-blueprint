using Enigmatry.Blueprint.Api.Features.Products;
using Enigmatry.Blueprint.Api.Features.Validations;
using Enigmatry.Blueprint.Model.Products;
using Enigmatry.CodeGeneration.Configuration.Form;
using Enigmatry.CodeGeneration.Configuration.Form.Model;

namespace Enigmatry.Blueprint.CodeGeneration.Setup.Features.Products
{
    public class ProductEditComponentConfiguration : IFormComponentConfiguration<GetProductDetails.Response>
    {
        public void Configure(FormComponentBuilder<GetProductDetails.Response> builder)
        {
            // Configuring component:
            builder.Component()
                .HasName("ProductEdit")
                .BelongsToFeature("Products");

            // Configuring fields:
            builder.FormControl(x => x.Name)
                .WithPlaceholder("Unique product name")
                .WithValidator("productNameIsUnique")
                .WithAppearance(FormControlAppearance.Outline);
            builder.FormControl(x => x.Code)
                .WithPlaceholder("Unique product code identifier")
                .WithValidator("productCodeIsUnique");
            builder.FormControl(x => x.Type)
                .IsDropDownListControl(options => options.WithFixedValues<ProductType>());
            builder.FormControl(x => x.Price)
                .WithLabel("Price per unit")
                .WithLabelTranslationId(ProductTranslationId.Price)
                .WithPlaceholder("Price per unit")
                .WithPlaceholderTranslationId(ProductTranslationId.Price);
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
            builder.FormControl(x => x.ExpiresOn)
                .WithPlaceholder("Product expiration date, if any")
                .WithValidator("productExpiresOnIsRequired");

            // Configuring built in validations
            builder.WithValidationConfiguration(new ProductEditComponentValidationConfiguration());
        }
    }
}
