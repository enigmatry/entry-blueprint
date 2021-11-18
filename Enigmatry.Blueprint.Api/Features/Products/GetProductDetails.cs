using AutoMapper;
using Enigmatry.Blueprint.Model.Products;
using Enigmatry.BuildingBlocks.Core.Data;
using Enigmatry.BuildingBlocks.Core.Entities;
using Enigmatry.BuildingBlocks.Core.EntityFramework;
using JetBrains.Annotations;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Enigmatry.Blueprint.Api.Features.Products
{
    public static class GetProductDetails
    {
        [PublicAPI]
        public class Request : IRequest<Response>
        {
            public Guid Id { get; set; }
        }

        [PublicAPI]
        public class Response
        {
            public Guid Id { get; set; }
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
            public bool HasDiscount { get; set; }
            public float Discount { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<Product, Response>();
        }

        [UsedImplicitly]
        public class RequestHanlder : IRequestHandler<Request, Response>
        {
            private readonly IRepository<Product> _productRepository;
            private readonly IMapper _mapper;

            public RequestHanlder(IRepository<Product> productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var response = await _productRepository.QueryAll()
                    .QueryById(request.Id)
                    .SingleOrDefaultMappedAsync<Product, Response>(_mapper, cancellationToken: cancellationToken);
                return response;
            }
        }
    }
}
