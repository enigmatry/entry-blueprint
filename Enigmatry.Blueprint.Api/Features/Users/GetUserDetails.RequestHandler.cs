using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Enigmatry.Blueprint.BuildingBlocks.Core.Data;
using Enigmatry.Blueprint.BuildingBlocks.Core.Entities;
using Enigmatry.Blueprint.BuildingBlocks.Core.EntityFramework;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Blueprint.Api.Features.Users
{
    public partial class GetUserDetails
    {
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
    }
}

