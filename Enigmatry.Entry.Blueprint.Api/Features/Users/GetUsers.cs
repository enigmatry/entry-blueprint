using AutoMapper;
using AutoMapper.QueryableExtensions;
using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Core.Cqrs;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.EntityFramework;
using Enigmatry.Entry.Core.Paging;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Api.Features.Users;

public static class GetUsers
{
    [PublicAPI]
    public class Request : PagedRequest<Response.Item>, IQuery<PagedResponse<Response.Item>>
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
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
            public string UserStatusName { get; set; } = String.Empty;
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
    public class RequestHandler(IRepository<User> repository, IMapper mapper) : IPagedRequestHandler<Request, Response.Item>
    {
        public async Task<PagedResponse<Response.Item>> Handle(Request request, CancellationToken cancellationToken) =>
            await repository.QueryAll()
                .Include(u => u.UserStatus)
                .QueryByName(request.Name)
                .QueryByEmail(request.Email)
                .ProjectTo<Response.Item>(mapper.ConfigurationProvider, cancellationToken)
                .ToPagedResponseAsync(request, cancellationToken);
    }
}
