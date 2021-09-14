using Enigmatry.Blueprint.Model.Products;
using Enigmatry.BuildingBlocks.Core.Data;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Enigmatry.Blueprint.Api.Features.Products
{
    public static class GetProductCodeUniquenes
    {
        [PublicAPI]
        public class Request : IRequest<Response>
        {
            public string Code { get; set; } = String.Empty;
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
                var response = await _productRepository.QueryAll()
                    .SingleOrDefaultAsync(x => x.Code == request.Code, cancellationToken);
                return new Response { IsUnique = response == null };
            }
        }
    }
}
