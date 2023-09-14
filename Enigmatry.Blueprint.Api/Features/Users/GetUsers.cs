using AutoMapper;
using AutoMapper.QueryableExtensions;
using Enigmatry.Blueprint.Domain.Users;
using Enigmatry.Entry.Core.Paging;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.EntityFramework;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Features.Users;

public static class GetUsers
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
            public string EmailAddress { get; set; } = String.Empty;
            public string FullName { get; set; } = String.Empty;
            public DateTimeOffset CreatedOn { get; set; }
            public DateTimeOffset UpdatedOn { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<User, Item>();
        }
    }

    [UsedImplicitly]
    public class RequestHandler : IPagedRequestHandler<Request, Response.Item>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public RequestHandler(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<Response.Item>> Handle(Request request, CancellationToken cancellationToken) =>
            await _repository.QueryAll()
                .QueryByKeyword(request.Keyword)
                .ProjectTo<Response.Item>(_mapper.ConfigurationProvider, cancellationToken)
                .ToPagedResponseAsync(request, cancellationToken);
    }
}
