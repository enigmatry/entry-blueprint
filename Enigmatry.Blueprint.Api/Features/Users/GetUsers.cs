using System;
using AutoMapper;
using Enigmatry.Blueprint.BuildingBlocks.Core.Paging;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Features.Users
{
    public static partial class GetUsers
    {
        [PublicAPI]
        public class Request : PagedRequest<Response.Item>
        {
            public string Keyword { get; set; } = String.Empty;
        }

        [PublicAPI]
        public static class Response
        {
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
    }
}
