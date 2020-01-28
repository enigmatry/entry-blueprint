using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Blueprint.Api.Features.Users
{
    public static partial class GetUsers
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
            public string Keyword { get; set; } = String.Empty;
        }

        [PublicAPI]
        public class Response
        {
            public IEnumerable<Item> Items { get; set; } = new List<Item>();
             
            [PublicAPI]
            public class Item
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
                public MappingProfile() => CreateMap<User, Item>();
            }
        }

        private static IQueryable<User> BuildInclude(this IQueryable<User> query)
        {
            return query;
        }
    }
}
