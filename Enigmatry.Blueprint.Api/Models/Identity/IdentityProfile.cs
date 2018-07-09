using AutoMapper;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Models.Identity
{
    [UsedImplicitly]
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<User, UserModel>();
        }
    }
}
