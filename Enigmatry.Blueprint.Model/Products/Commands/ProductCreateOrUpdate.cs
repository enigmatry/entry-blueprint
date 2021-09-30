using JetBrains.Annotations;
using MediatR;
using System;

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
    }
}
