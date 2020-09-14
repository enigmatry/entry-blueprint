using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Blueprint.Api.Features.Users
{
    public partial class GetUserDetails
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<User> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<User> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query query, CancellationToken cancellationToken)
            {
                var response = await _repository.QueryAll()
                    .BuildInclude()
                    .QueryById(query.Id)
                    .SingleOrDefaultMappedAsync<User, Response>(_mapper, cancellationToken: cancellationToken);
                return response;
            }
        }
    }
}

