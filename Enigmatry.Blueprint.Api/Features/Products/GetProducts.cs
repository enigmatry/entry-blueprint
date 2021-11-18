using AutoMapper;
using AutoMapper.QueryableExtensions;
using Enigmatry.Blueprint.Model.Products;
using Enigmatry.BuildingBlocks.Core.Data;
using Enigmatry.BuildingBlocks.Core.EntityFramework;
using Enigmatry.BuildingBlocks.Core.Paging;
using JetBrains.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Enigmatry.Blueprint.Api.Features.Products
{
    public static class GetProducts
    {
        [PublicAPI]
        public class Request : PagedRequest<Response.Item>
        {
            public string Keyword { get; set; } = String.Empty;
        }

        [PublicAPI]
        public static class Response
        {
            [PublicAPI]
            public class Item
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
                public MappingProfile() => CreateMap<Product, Item>();
            }
        }

        [UsedImplicitly]
        public class RequestHanlder : IPagedRequestHandler<Request, Response.Item>
        {
            private readonly IRepository<Product> _productRepository;
            private readonly IMapper _mapper;

            public RequestHanlder(IRepository<Product> productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<PagedResponse<Response.Item>> Handle(Request request, CancellationToken cancellationToken) =>
                await _productRepository.QueryAll()
                    .ProjectTo<Response.Item>(_mapper.ConfigurationProvider, cancellationToken)
                    .ToPagedResponseAsync(request, cancellationToken);
        }
    }
}
