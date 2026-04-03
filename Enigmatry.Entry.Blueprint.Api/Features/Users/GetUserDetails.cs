using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Core.Cqrs;
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
    public class Request : IQuery<Response>
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
    public class RequestHandler(IRepository<User> repository) : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var response = await repository.QueryAll()
                .BuildInclude()
                .QueryById(request.Id)
                .MapToUserDetailsItem()
                .SingleOrNotFoundAsync(cancellationToken);
            return response;
        }
    }

    public static IQueryable<Response> MapToUserDetailsItem(this IQueryable<User> query) =>
        query.Select(x => new Response
        {
            Id = x.Id,
            CreatedOn = x.CreatedOn,
            UpdatedOn = x.UpdatedOn,
            EmailAddress = x.EmailAddress,
            FullName = x.FullName,
            RoleId = x.RoleId,
            UserStatusId = x.UserStatusId,
            UserStatusName = x.UserStatus.Name,
            UserStatusDescription = x.UserStatus.Description
        });

    private static IQueryable<User> BuildInclude(this IQueryable<User> query) => query.Include(x => x.UserStatus);
}
