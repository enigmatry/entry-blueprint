using Enigmatry.Entry.Core.Data;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Entry.Blueprint.Domain.Products.Commands;

public static class ProductCreateOrUpdate
{
    [PublicAPI]
    public class Command : IRequest<Result>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Code { get; set; } = String.Empty;
        public ProductType Type { get; set; }
        public string Description { get; set; } = String.Empty;
        public double Price { get; set; }
        public int Amount { get; set; }
        public string ContactEmail { get; set; } = String.Empty;
        public string ContactPhone { get; set; } = String.Empty;
        public string InfoLink { get; set; } = String.Empty;
        public DateTimeOffset? ExpiresOn { get; set; }
        public bool FreeShipping { get; set; }
        public bool HasDiscount { get; set; }
        public float? Discount { get; set; }
    }

    [PublicAPI]
    public class Result
    {
        public Guid Id { get; set; }
    }

    [UsedImplicitly]
    public class Validator : AbstractValidator<Command>
    {
        private const string DynamicMessagesID = "[MESSAGE_TEMPLATE]";
        private readonly IRepository<Product> _productRepository;

        public Validator(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;

            RuleFor(x => x.Name).NotEmpty().MaximumLength(Product.NameMaxLength);
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Code)
                .Must((command, code, context) => HaveValidCodePrefix(code, command, context))
                .WithMessage($"{{{DynamicMessagesID}}}");
            RuleFor(x => x.Price).GreaterThan(Product.PriceMinValue).LessThanOrEqualTo(Product.PriceMaxValue);
            RuleFor(x => x.Amount).GreaterThan(Product.AmountMinValue);
            RuleFor(x => x.ContactEmail).NotEmpty().EmailAddress();
            RuleFor(x => x.ContactPhone).NotEmpty();
            When(x => x.Type is ProductType.Food or ProductType.Drink, () =>
            {
                RuleFor(x => x.ExpiresOn).NotNull();
            });
            When(x => x.HasDiscount, () =>
            {
                RuleFor(x => x.Discount)
                    .NotNull()
                    .GreaterThanOrEqualTo(Product.DiscountMinValue)
                    .LessThanOrEqualTo(Product.DiscountMaxValue);
            });
            RuleFor(x => x)
                .Must(HaveFreeSpaceInStorageRoom)
                .WithMessage(command => $"There is not enought space in storage room from {command.Amount} units of {command.Type}.");
        }

        private static bool HaveValidCodePrefix(string code, Command command, ValidationContext<Command> context)
        {
            if (command.Type == ProductType.Food && !code.StartsWith("FDXX", StringComparison.InvariantCulture))
            {
                context.MessageFormatter.AppendArgument(DynamicMessagesID, $"{command.Type} products code must begin with 'FDXX' prefix.");
                return false;
            }
            else if (command.Type == ProductType.Drink! && code.StartsWith("DKXX", StringComparison.InvariantCulture))
            {
                context.MessageFormatter.AppendArgument(DynamicMessagesID, $"{command.Type} products code must begin with 'DKXX' prefix.");
                return false;
            }
            else if (command.Type == ProductType.Book && !code.StartsWith("BKXX", StringComparison.InvariantCulture))
            {
                context.MessageFormatter.AppendArgument(DynamicMessagesID, $"{command.Type} products code must begin with 'BKXX' prefix.");
                return false;
            }
            else if (command.Type == ProductType.Car && !code.StartsWith("CRXX", StringComparison.InvariantCulture))
            {
                context.MessageFormatter.AppendArgument(DynamicMessagesID, $"{command.Type} products code must begin with 'CRXX' prefix.");
                return false;
            }
            return true;
        }

        private bool HaveFreeSpaceInStorageRoom(Command command)
        {
            var productTotalAmmount = _productRepository.QueryAllSkipCache()
                .Where(x => x.Type == command.Type)
                .Sum(x => x.Amount);
            var newTotalAmmount = command.Amount + productTotalAmmount;
            return command.Type == ProductType.Book
                ? newTotalAmmount <= 10000
                : command.Type == ProductType.Car
                    ? newTotalAmmount <= 25
                    : command.Type == ProductType.Food
                        ? newTotalAmmount <= 5500
                        : newTotalAmmount <= 7000;
        }

    }
}
