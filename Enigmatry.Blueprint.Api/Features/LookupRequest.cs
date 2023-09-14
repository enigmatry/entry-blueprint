using MediatR;

namespace Enigmatry.Blueprint.Api.Features
{
    public class LookupRequest<T> : IRequest<IEnumerable<LookupResponse<T>>>
    {
    }
}
