using AutoMapper;
using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Blueprint.Domain.Identity;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Blueprint.Api.Features.Authorization;

public static class GetUserProfile
{
    public class Request : IRequest<Response>
    {

    }

    [PublicAPI]
    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public RoleProfile Role { get; set; } = null!;

        [PublicAPI]
        public class RoleProfile
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = String.Empty;
            public ICollection<PermissionProfile> Permissions { get; set; } = Enumerable.Empty<PermissionProfile>().ToList();
        }

        [PublicAPI]
        public class PermissionProfile
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = String.Empty;
        }
    }

    [UsedImplicitly]
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, Response>();
            CreateMap<Role, Response.RoleProfile>();
            CreateMap<Permission, Response.PermissionProfile>();
        }
    }

    [UsedImplicitly]
    public class RequestHandler : IRequestHandler<Request, Response>
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserProvider _currentUserProvider;

        public RequestHandler(IMapper mapper, ICurrentUserProvider currentUserIdProvider)
        {
            _mapper = mapper;
            _currentUserProvider = currentUserIdProvider;
        }
        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = _currentUserProvider.User;
            if (user == null)
            {
                throw new InvalidOperationException("Can't load current user");
            }

            return Task.FromResult(_mapper.Map<Response>(user));
        }
    }
}
