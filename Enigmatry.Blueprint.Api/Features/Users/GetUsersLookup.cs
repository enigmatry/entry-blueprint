using AutoMapper;
using AutoMapper.QueryableExtensions;
using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.EntityFramework;
using Enigmatry.Entry.Core.Paging;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Features.Users;
public static class GetUsersLookup
{
    [PublicAPI]
    public class Request : PagedRequest<LookupResponse> { }

    [UsedImplicitly]
    public class MappingProfile : Profile
    {
        public MappingProfile() => CreateMap<User, LookupResponse>()
            .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.DisplayName, opt => opt.MapFrom(s => s.Name));
    }

    [UsedImplicitly]
    public class RequestHandler : IPagedRequestHandler<Request, LookupResponse>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public RequestHandler(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<LookupResponse>> Handle(Request request, CancellationToken cancellationToken) =>
            await _repository.QueryAllSkipCache()
                .ProjectTo<LookupResponse>(_mapper.ConfigurationProvider, cancellationToken)
                .ToPagedResponseAsync(request, cancellationToken);
    }
}
