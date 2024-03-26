using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.EntityFramework;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Domain.Products.Commands;

public static class ProductUpdateAmount
{
    public class Command : IRequest;

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
            var products = await _repository.QueryAll().ToListAsync(cancellationToken);
            products.ForEach(product => product.DecreaseAmount());
        }
    }
}
