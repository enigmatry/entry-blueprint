using System;
using System.Linq;
using AutoMapper;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Blueprint.Api.Features.Users
{
    public static partial class GetUserDetails
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
            public Guid Id { get; set; }
            public int? OrganizationId { get; set;  }
            public static Query ById(Guid id) => new Query {Id = id};


        }

        [PublicAPI]
        public class Response
        {
            public Guid Id { get; set; }
            public string UserName { get; set; } = String.Empty;
            public string Name { get; set; } = String.Empty;
            public DateTimeOffset CreatedOn { get; set; }
            public DateTimeOffset UpdatedOn { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<User, Response>();
        }

        private static IQueryable<User> BuildInclude(this IQueryable<User> query)
        {
            return query;
        }
    }
}
