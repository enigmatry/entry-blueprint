using MediatR;

namespace Enigmatry.Entry.Blueprint.Api.Features
{
    public class LookupRequest<T> : IRequest<IEnumerable<LookupResponse<T>>>
    {
    }
}
