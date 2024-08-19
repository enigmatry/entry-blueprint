using AutoMapper;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.Entities;
using Enigmatry.Entry.Core.EntityFramework;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Api.Features.Authorization;

public static class GetUserProfile
{
    public class Request : IRequest<Response>
    {
    }

    [PublicAPI]
    public class Response
    {
        public Guid Id { get; init; }
        public string FullName { get; init; } = String.Empty;
        public string EmailAddress { get; init; } = String.Empty;
        public IEnumerable<PermissionId> Permissions { get; init; } = [];
    }

    [UsedImplicitly]
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, Response>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(user => user.GetPermissionIds()));
        }
    }

    [UsedImplicitly]
    public class RequestHandler(IMapper mapper, ICurrentUserProvider currentUserProvider, IRepository<User> repository)
        : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = await repository.QueryAll().QueryById(currentUserProvider.UserId!.Value)
                .Include(u => u.Role).ThenInclude(r => r.Permissions)
                .Include(u => u.Status)
                .SingleOrNotFoundAsync(cancellationToken);
            var response = mapper.Map<Response>(user);
            return response;
        }
    }
}
