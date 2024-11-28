using AutoMapper;
using AutoMapper.QueryableExtensions;
using Enigmatry.Entry.Blueprint.Domain.Products;
using Enigmatry.Entry.Core.Cqrs;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.EntityFramework;
using Enigmatry.Entry.Core.Paging;
using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.Api.Features.Products;

public static class GetProducts
{
    [PublicAPI]
    public class Request : PagedRequest<Response.Item>, IQuery<PagedResponse<Response.Item>>
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateOnly? ExpiresBefore { get; set; }
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
            public DateOnly? ExpiresOn { get; set; }
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
    public class RequestHandler : IPagedRequestHandler<Request, Response.Item>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public RequestHandler(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<Response.Item>> Handle(Request request, CancellationToken cancellationToken) =>
            await _productRepository.QueryAll()
                .QueryByCode(request.Code)
                .QueryByName(request.Name)
                .QueryExpiresBefore(request.ExpiresBefore)
                .ProjectTo<Response.Item>(_mapper.ConfigurationProvider, cancellationToken)
                .ToPagedResponseAsync(request, cancellationToken);
    }
}
