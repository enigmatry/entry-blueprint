using AutoMapper;
using Enigmatry.Entry.Blueprint.Domain.Products;
using Enigmatry.Entry.Core.Cqrs;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.Entities;
using Enigmatry.Entry.Core.EntityFramework;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Entry.Blueprint.Api.Features.Products;

public static class GetProductDetails
{
    [PublicAPI]
    public class Request : IQuery<Response>
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
        public string Description { get; set; } = String.Empty;
        public double Price { get; set; }
        public int Amount { get; set; }
        public string ContactEmail { get; set; } = String.Empty;
        public string ContactPhone { get; set; } = String.Empty;
        public string InfoLink { get; set; } = String.Empty;
        public string AdditionalInfo { get; set; } = String.Empty;
        public DateOnly? ExpiresOn { get; set; }
        public bool HasDiscount { get; set; }
        public float Discount { get; set; }
        public bool FreeShipping { get; set; }
    }

    [UsedImplicitly]
    public class MappingProfile : Profile
    {
        public MappingProfile() => CreateMap<Product, Response>().ForMember(x => x.AdditionalInfo, opt => opt.Ignore());
    }

    [UsedImplicitly]
    public class RequestHandler(IRepository<Product> productRepository, IMapper mapper) : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var response = await productRepository.QueryAll()
                .QueryById(request.Id)
                .SingleOrDefaultMappedAsync<Product, Response>(mapper, cancellationToken: cancellationToken);
            return response;
        }
    }
}
