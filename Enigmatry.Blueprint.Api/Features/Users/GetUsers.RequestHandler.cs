using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Blueprint.Api.Features.Users
{
    public partial class GetUsers
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

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _repository.QueryAll()
                    .QueryByKeyword(request.Keyword)
                    .BuildInclude()
                    .ToListMappedAsync<User, Response.Item>(_mapper, cancellationToken);

                return new Response { Items = items };
            }
        }
    }
}
