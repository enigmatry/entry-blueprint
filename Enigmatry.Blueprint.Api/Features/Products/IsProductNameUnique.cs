using Enigmatry.Blueprint.Model.Products;
using Enigmatry.BuildingBlocks.Core.Data;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Enigmatry.Blueprint.Api.Features.Products
{
    public static class IsProductNameUnique
    {
        [PublicAPI]
        public class Request : IRequest<Response>
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = String.Empty;
        }

        [PublicAPI]
        public class Response
        {
            public bool IsUnique { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IRepository<Product> _productRepository;

            public RequestHandler(IRepository<Product> productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var isUnique = !await _productRepository.QueryAll()
                    .Where(x => x.Id != request.Id)
                    .AnyAsync(x => x.Name == request.Name, cancellationToken);
                return new Response { IsUnique = isUnique };
            }
        }
    }
}
