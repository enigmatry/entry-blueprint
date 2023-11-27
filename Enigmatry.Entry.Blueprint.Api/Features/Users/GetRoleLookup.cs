using AutoMapper;
using AutoMapper.QueryableExtensions;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Core.Data;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Api.Features.Users
{
    public static class GetRoleLookup
    {
        [PublicAPI]
        public class Request : LookupRequest<Guid>
        {
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Role, LookupResponse<Guid>>()
                    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));
            }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Request, IEnumerable<LookupResponse<Guid>>>
        {
            private readonly IMapper _mapper;
            private readonly IRepository<Role> _roleRepository;

            public RequestHandler(IMapper mapper, IRepository<Role> roleRepository)
            {
                _mapper = mapper;
                _roleRepository = roleRepository;
            }

            public async Task<IEnumerable<LookupResponse<Guid>>> Handle(Request request, CancellationToken cancellationToken) =>
                await _roleRepository
                    .QueryAll()
                    .ProjectTo<LookupResponse<Guid>>(_mapper.ConfigurationProvider, cancellationToken)
                    .OrderBy(r => r.Label)
                    .ToListAsync(cancellationToken);
        }
    }
}
