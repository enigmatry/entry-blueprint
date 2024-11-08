using Enigmatry.Entry.Core.Cqrs;

namespace Enigmatry.Entry.Blueprint.Api.Features;

public class LookupRequest<T> : IQuery<IEnumerable<LookupResponse<T>>>;
