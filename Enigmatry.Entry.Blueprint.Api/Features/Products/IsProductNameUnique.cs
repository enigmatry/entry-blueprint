using Enigmatry.Entry.Blueprint.Domain.Products;
using Enigmatry.Entry.Core.Cqrs;
using Enigmatry.Entry.Core.Data;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Api.Features.Products;

public static class IsProductNameUnique
{
    [PublicAPI]
    public class Request : IQuery<Response>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = String.Empty;
    }

    [PublicAPI]
    public class Response
    {
        public bool IsUnique { get; set; }
    }

    [UsedImplicitly]
    public class RequestHandler(IRepository<Product> productRepository) : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var isUnique = !await productRepository.QueryAll()
                .Where(x => x.Id != request.Id)
                .AnyAsync(x => x.Name == request.Name, cancellationToken);
            return new Response { IsUnique = isUnique };
        }
    }
}
