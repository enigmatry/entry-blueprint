using Enigmatry.Blueprint.Api.Features.Products;
using Enigmatry.Blueprint.Model.Products;
using Enigmatry.BuildingBlocks.Validation;
using System.Text.RegularExpressions;

namespace Enigmatry.Blueprint.CodeGeneration.Setup.Features.Products
{
    internal class ProductEditValidationConfiguration : ValidationConfiguration<GetProductDetails.Response>
    {
        public ProductEditValidationConfiguration()
        {
            RuleFor(x => x.Name).IsRequired().Max(Product.NameMaxLength);
            RuleFor(x => x.Code)
                .IsRequired()
                .HasPattern(new Regex("/^[A-Z]{4}[1-9]{8}$/mu"))
                .WithMessage("Code must be in 4 letter 8 digits format (e.g. ABCD12345678)")
                .HasAsyncValidator("ProductCodeUniquenessValidator")
                .WithMessage("Code is not unique");
            RuleFor(x => x.Price).IsRequired().Min((int)Product.PriceMinValue);
            RuleFor(x => x.Type).IsRequired();
            RuleFor(x => x.ContactEmail).IsRequired().IsEmailAddress();
            RuleFor(x => x.ContactPhone).IsRequired().HasValidator("PhoneNumberValidator");
        }
    }
}
