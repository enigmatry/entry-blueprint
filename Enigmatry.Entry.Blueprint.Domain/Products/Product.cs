using Enigmatry.Entry.Blueprint.Domain.Products.Commands;
using Enigmatry.Entry.Blueprint.Domain.Products.DomainEvents;

namespace Enigmatry.Entry.Blueprint.Domain.Products;

public class Product : EntityWithCreatedUpdated
{
    public const int NameMinLength = 5;
    public const int NameMaxLength = 50;
    public const int CodeMaxLength = 12;
    public const double PriceMinValue = 0.99;
    public const double PriceMaxValue = 999.99;
    public const int AmountMinValue = 0;
    public const int ContactEmailMaxLength = 50;
    public const int ContactPhoneMaxLength = 25;
    public const float DiscountMinValue = 0.0F;
    public const float DiscountMaxValue = 100.0F;
    public const int DescriptionMaxLength = 1500;

    public static readonly Guid TestProductId = new("8A690056-DE31-4203-830F-8E08E4A22A75");
    public string Name { get; private set; } = String.Empty;
    public string Code { get; private set; } = String.Empty;
    public ProductType Type { get; private set; } = ProductType.Food;
    public string Description { get; set; } = String.Empty;
    public double Price { get; private set; }
    public int Amount { get; private set; }
    public string ContactEmail { get; private set; } = String.Empty;
    public string ContactPhone { get; private set; } = String.Empty;
    public string InfoLink { get; private set; } = String.Empty;
    public DateOnly? ExpiresOn { get; private set; }
    public bool FreeShipping { get; private set; }
    public bool HasDiscount { get; private set; }
    public float? Discount { get; private set; }

    public static Product Create(ProductCreateOrUpdate.Command request)
    {
        var product = new Product
        {
            Name = request.Name,
            Code = request.Code,
            Type = request.Type,
            Description = request.Description,
            Price = request.Price,
            Amount = request.Amount,
            ContactEmail = request.ContactEmail,
            ContactPhone = request.ContactPhone,
            InfoLink = request.InfoLink,
            ExpiresOn = request.ExpiresOn,
            FreeShipping = request.FreeShipping,
            HasDiscount = request.HasDiscount,
            Discount = request.Discount
        };
        product.AddDomainEvent(new ProductCreatedDomainEvent(product));
        return product;
    }

    public void Update(ProductCreateOrUpdate.Command request)
    {
        Name = request.Name;
        Code = request.Code;
        Type = request.Type;
        Description = request.Description;
        Price = request.Price;
        Amount = request.Amount;
        ContactEmail = request.ContactEmail;
        ContactPhone = request.ContactPhone;
        InfoLink = request.InfoLink;
        ExpiresOn = request.ExpiresOn;
        FreeShipping = request.FreeShipping;
        HasDiscount = request.HasDiscount;
        Discount = HasDiscount ? request.Discount : null;
        AddDomainEvent(new ProductUpdatedDomainEvent(this));
    }

    public void UpdateAmount(int value)
    {
        Amount = value;
        AddDomainEvent(new ProductUpdatedDomainEvent(this));
    }
}
