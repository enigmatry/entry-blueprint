using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Enigmatry.Blueprint.BuildingBlocks.Core.Data;
using Enigmatry.Blueprint.BuildingBlocks.Core.EntityFramework;
using Enigmatry.Blueprint.BuildingBlocks.Core.Paging;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Features.Users
{
    public partial class GetUsers
    {
        [UsedImplicitly]
        public class RequestHandler : IPagedRequestHandler<Request, Response.Item>
        {
            private readonly IRepository<User> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<User> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<PagedResponse<Response.Item>> Handle(Request request, CancellationToken cancellationToken)
            {
                return await _repository.QueryAll()
                    .QueryByKeyword(request.Keyword)
                    .ProjectTo<Response.Item>(_mapper.ConfigurationProvider, cancellationToken)
                    .ToPagedResponseAsync(request, cancellationToken);
            }
        }
    }
}
