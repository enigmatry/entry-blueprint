using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Models.Validation
{
    [PublicAPI]
    public class ErrorModel
    {
        public string Key { get; set; }
        public string ErrorMessage { get; set; }
    }
}