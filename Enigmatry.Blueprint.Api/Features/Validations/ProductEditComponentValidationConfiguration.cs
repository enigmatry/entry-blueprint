using Enigmatry.Blueprint.Api.Features.Products;
using Enigmatry.Blueprint.Model.Products;
using Enigmatry.BuildingBlocks.Validation;
using System.Text.RegularExpressions;

namespace Enigmatry.Blueprint.Api.Features.Validations
{
    public class ProductEditComponentValidationConfiguration : ValidationConfiguration<GetProductDetails.Response>
    {
        public ProductEditComponentValidationConfiguration()
        {
            RuleFor(x => x.Name)
                .IsRequired()
                .MinLength(Product.NameMinLength)
                .MaxLength(Product.NameMaxLength);
            RuleFor(x => x.Code)
                .IsRequired()
                .Match(new Regex("/^[A-Z]{4}[1-9]{8}$/mu"))
                .WithMessage("Code must be in 4 letter 8 digits format (e.g. ABCD12345678)");
            RuleFor(x => x.Price)
                .IsRequired()
                .GreaterThen(Product.PriceMinValue)
                .LessOrEqualTo(Product.PriceMaxValue);
            RuleFor(x => x.Amount)
                .IsRequired()
                .GreaterThen(Product.AmountMinValue)
                .LessOrEqualTo(Product.AmountMaxValue);
            RuleFor(x => x.Type)
                .IsRequired();
            RuleFor(x => x.ContactEmail)
                .IsRequired()
                .EmailAddress();
            RuleFor(x => x.ContactPhone)
                .IsRequired()
                .Match(new Regex("/^s*(?:\\+?(\\d{1,3}))?[-. (]*(\\d{3})[-. )]*(\\d{3})[-. ]*(\\d{4})(?: *x(\\d+))?\\s*$/mu"));
        }
    }
}
