using Enigmatry.BuildingBlocks.Core.Helpers;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using System;
using System.Text.RegularExpressions;

namespace Enigmatry.Blueprint.Model.Products.Commands
{
    public static class ProductCreateOrUpdate
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            public Guid? Id { get; set; }
            public string Name { get; set; } = String.Empty;
            public string Code { get; set; } = String.Empty;
            public ProductType Type { get; set; }
            public double Price { get; set; }
            public int Amount { get; set; }
            public string ContactEmail { get; set; } = String.Empty;
            public string ContactPhone { get; set; } = String.Empty;
            public string InfoLink { get; set; } = String.Empty;
            public DateTimeOffset? ExpiresOn { get; set; }
            public bool FreeShipping { get; set; }
        }

        [PublicAPI]
        public class Result
        {
            public Guid Id { get; set; }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(Product.NameMaxLength);
                RuleFor(x => x.Code).NotEmpty();
                RuleFor(x => x.Price).GreaterThan(Product.PriceMinValue).LessThanOrEqualTo(Product.PriceMaxValue);
                RuleFor(x => x.Amount).GreaterThan(Product.AmountMinValue).LessThanOrEqualTo(Product.AmountMaxValue);
                RuleFor(x => x.ContactEmail).NotEmpty().EmailAddress();
                RuleFor(x => x.ContactPhone).NotEmpty();
                When(x => x.Type is ProductType.Food or ProductType.Drink, () =>
                {
                    RuleFor(x => x.ExpiresOn).NotNull();
                });
            }
        }
    }
}
