using System.Text.RegularExpressions;
using Enigmatry.Entry.Blueprint.Api.Features.Products;
using Enigmatry.Entry.Blueprint.Domain.Products;
using Enigmatry.Entry.CodeGeneration.Validation;

namespace Enigmatry.Entry.Blueprint.Api.Features.Validations;

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
            .GreaterThen(Product.AmountMinValue);
        RuleFor(x => x.Type)
            .IsRequired();
        RuleFor(x => x.ContactEmail)
            .IsRequired()
            .EmailAddress();
        RuleFor(x => x.ContactPhone)
            .IsRequired()
            .Match(new Regex("/^s*(?:\\+?(\\d{1,3}))?[-. (]*(\\d{3})[-. )]*(\\d{3})[-. ]*(\\d{4})(?: *x(\\d+))?\\s*$/mu"));
        RuleFor(x => x.InfoLink)
            .Match(new Regex(@"/https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\\+.~#?&//=]*)/u"));
        RuleFor(x => x.Discount)
            .GreaterOrEqualTo(Product.DiscountMinValue)
            .LessOrEqualTo(Product.DiscountMaxValue);
    }
}
