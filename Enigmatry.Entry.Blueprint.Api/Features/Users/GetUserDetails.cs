using AutoMapper;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.Entities;
using Enigmatry.Entry.Core.EntityFramework;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Api.Features.Users;

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
        public UserStatusId UserStatusId { get; set; } = UserStatusId.Active;
        public string UserStatusName { get; set; } = String.Empty;
        public string UserStatusDescription { get; set; } = String.Empty;
    }

    [UsedImplicitly]
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, Response>();
        }
    }

    [UsedImplicitly]
    public class RequestHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var response = await repository.QueryAll()
                .BuildInclude()
                .QueryById(request.Id)
                .SingleOrDefaultMappedAsync<User, Response>(mapper, cancellationToken);
            return response;
        }
    }

    private static IQueryable<User> BuildInclude(this IQueryable<User> query) => query.Include(x => x.UserStatus);
}
