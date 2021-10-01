using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Model.Identity;
using Enigmatry.Blueprint.Model.Products.Commands;
using Enigmatry.Blueprint.Model.Products.DomainEvents;
using Enigmatry.BuildingBlocks.Core.Entities;
using System;

namespace Enigmatry.Blueprint.Model.Products
{
    public class Product : EntityWithGuidId, IEntityHasCreatedUpdated
    {
        public const int NameMinLength = 5;
        public const int NameMaxLength = 50;
        public const int CodeMaxLength = 12;
        public const double PriceMinValue = 0.99;
        public const double PriceMaxValue = 999.99;
        public const int AmountMinValue = 0;
        public const int AmountMaxValue = 100;
        public const int ContactEmailMaxLength = 50;
        public const int ContactPhoneMaxLength = 25;

        public string Name { get; private set; } = String.Empty;
        public string Code { get; private set; } = String.Empty;
        public ProductType Type { get; private set; } = ProductType.Food;
        public double Price { get; private set; }
        public int Amount { get; private set; }
        public string ContactEmail { get; private set; } = String.Empty;
        public string ContactPhone { get; private set; } = String.Empty;
        public string InfoLink { get; private set; } = String.Empty;
        public DateTimeOffset? ExpiresOn { get; private set; }
        public bool FreeShipping { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset UpdatedOn { get; private set; }
        public Guid? CreatedById { get; private set; }
        public Guid? UpdatedById { get; private set; }
        public User? CreatedBy { get; private set; }
        public User? UpdatedBy { get; private set; }


        public static Product Create(ProductCreateOrUpdate.Command request)
        {
            var product = new Product
            {
                Name = request.Name,
                Code = request.Code,
                Type = request.Type,
                Price = request.Price,
                Amount = request.Amount,
                ContactEmail = request.ContactEmail,
                ContactPhone = request.ContactPhone,
                InfoLink = request.InfoLink,
                ExpiresOn = request.ExpiresOn,
                FreeShipping = request.FreeShipping
            };
            product.AddDomainEvent(new ProductCreatedDomainEvent(product));
            return product;
        }

        public void Update(ProductCreateOrUpdate.Command request)
        {
            Name = request.Name;
            Code = request.Code;
            Type = request.Type;
            Price = request.Price;
            Amount = request.Amount;
            ContactEmail = request.ContactEmail;
            ContactPhone = request.ContactPhone;
            InfoLink = request.InfoLink;
            ExpiresOn = request.ExpiresOn;
            FreeShipping = request.FreeShipping;
            AddDomainEvent(new ProductUpdatedDomainEvent(this));
        }

        public void SetCreated(DateTimeOffset createdOn, Guid createdBy)
        {
            SetCreated(createdOn);
            CreatedById = createdBy;
        }

        public void SetCreated(DateTimeOffset createdOn) => CreatedOn = createdOn;

        public void SetUpdated(DateTimeOffset updatedOn, Guid updatedBy)
        {
            SetUpdated(updatedOn);
            UpdatedById = updatedBy;
        }

        public void SetUpdated(DateTimeOffset updatedOn) => UpdatedOn = updatedOn;
    }
}
