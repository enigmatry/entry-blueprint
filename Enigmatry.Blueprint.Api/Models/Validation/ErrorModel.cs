using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Models.Validation
{
    [PublicAPI]
    public class ErrorModel
    {
        public string Field { get; set; }
        public string ErrorMessage { get; set; }
    }
}