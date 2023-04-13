using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Features
{
    [PublicAPI]
    public class LookupResponse
    {
        public object? Value { get; set; }
        public string DisplayName { get; set; } = String.Empty;
    }
}
