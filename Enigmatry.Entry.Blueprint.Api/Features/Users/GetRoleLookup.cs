﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Core.Data;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Api.Features.Users;

public static class GetRoleLookup
{
    [PublicAPI]
    public class Request : LookupRequest<Guid>;

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
    public class RequestHandler(IMapper mapper, IRepository<Role> roleRepository)
        : IRequestHandler<Request, IEnumerable<LookupResponse<Guid>>>
    {
        public async Task<IEnumerable<LookupResponse<Guid>>> Handle(Request request, CancellationToken cancellationToken) =>
            await roleRepository
                .QueryAll()
                .ProjectTo<LookupResponse<Guid>>(mapper.ConfigurationProvider, cancellationToken)
                .OrderBy(r => r.Label)
                .ToListAsync(cancellationToken);
    }
}
