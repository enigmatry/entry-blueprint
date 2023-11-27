using AutoMapper;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Blueprint.Domain.Users;
using JetBrains.Annotations;
using MediatR;

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
        public IEnumerable<PermissionId> Permissions { get; init; } = Enumerable.Empty<PermissionId>();
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
    public class RequestHandler : IRequestHandler<Request, Response>
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserProvider _currentUserProvider;

        public RequestHandler(IMapper mapper, ICurrentUserProvider currentUserProvider)
        {
            _mapper = mapper;
            _currentUserProvider = currentUserProvider;
        }

        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            => Task.FromResult(_mapper.Map<Response>(_currentUserProvider.User));
    }
}
