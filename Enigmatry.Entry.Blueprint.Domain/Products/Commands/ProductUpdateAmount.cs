using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.EntityFramework;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Entry.Blueprint.Domain.Products.Commands;

public static class ProductUpdateAmount
{
    public class Command : IRequest
    {
        public Guid Id { get; init; }
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
            var product = await _repository.QueryAll().Where(x => x.Id == request.Id).SingleOrNotFoundAsync(cancellationToken);
            product.DecreaseAmount();
        }
    }
}
