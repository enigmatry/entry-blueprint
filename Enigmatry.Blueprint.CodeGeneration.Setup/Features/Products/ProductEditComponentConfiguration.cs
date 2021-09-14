using Enigmatry.Blueprint.Api.Features.Products;
using Enigmatry.Blueprint.Model.Products;
using Enigmatry.CodeGeneration.Configuration.Form;

namespace Enigmatry.Blueprint.CodeGeneration.Setup.Features.Products
{
    public class ProductEditComponentConfiguration : IFormComponentConfiguration<GetProductDetails.Response>
    {
        public void Configure(FormComponentBuilder<GetProductDetails.Response> builder)
        {
            builder.Component()
                .HasName("ProductEdit")
                .BelongsToFeature("Products");

            builder.FormControl(x => x.Name).WithPlaceholder("Product name");
            builder.FormControl(x => x.Code).WithPlaceholder("Unique product identifier");
            builder.FormControl(x => x.Type).IsDropDownListControl().WithFixedValues<ProductType>();
            builder.FormControl(x => x.Price).WithPlaceholder("Product price");
            builder.FormControl(x => x.ContactEmail).WithPlaceholder("Contact person email address");
            builder.FormControl(x => x.ContactPhone).WithPlaceholder("Contact person phone number");
            builder.FormControl(x => x.ExpiresOn).WithPlaceholder("Product expiration date, if any");

            builder.WithValidationConfiguration(new ProductEditValidationConfiguration());
        }
    }
}
