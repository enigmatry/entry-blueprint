using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Features
{
    [PublicAPI]
    public class LookupResponse<T>
    {
        public required T Value { get; set; }
        public string Label { get; set; } = String.Empty;
    }
}
