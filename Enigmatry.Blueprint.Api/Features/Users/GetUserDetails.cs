using AutoMapper;
using Enigmatry.Blueprint.Domain.Users;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.Entities;
using Enigmatry.Entry.Core.EntityFramework;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Blueprint.Api.Features.Users;

public static class GetUserDetails
{
    [PublicAPI]
    public class Request : IRequest<Response>
    {
        public Guid Id { get; set; }
        public static Request ById(Guid id) => new() { Id = id };
    }

    [PublicAPI]
    public class Response
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; } = String.Empty;
        public string FullName { get; set; } = String.Empty;
        public Guid RoleId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
    }

    [UsedImplicitly]
    public class MappingProfile : Profile
    {
        public MappingProfile() => CreateMap<User, Response>();
    }

    [UsedImplicitly]
    public class RequestHandler : IRequestHandler<Request, Response>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public RequestHandler(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var response = await _repository.QueryAll()
                .BuildInclude()
                .QueryById(request.Id)
                .SingleOrDefaultMappedAsync<User, Response>(_mapper, cancellationToken: cancellationToken);
            return response;
        }
    }

    private static IQueryable<User> BuildInclude(this IQueryable<User> query) => query;
}
