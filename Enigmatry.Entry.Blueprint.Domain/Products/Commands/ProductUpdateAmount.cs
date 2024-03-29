using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.Entities;
using Enigmatry.Entry.Core.EntityFramework;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Domain.Products.Commands;

public static class ProductUpdateAmount
{
    public class Command : IRequest
    {
        public required Guid ProductId { get; set; }
        public required int Amount { get; set; }
    }

    [UsedImplicitly]
    public class RequestHandler : IRequestHandler<Command>
    {
        private readonly IRepository<Product> _repository;

        public RequestHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var product = await _repository.QueryAll().QueryById(request.ProductId).SingleOrNotFoundAsync(cancellationToken);
            product.UpdateAmount(request.Amount);
        }
    }
}
