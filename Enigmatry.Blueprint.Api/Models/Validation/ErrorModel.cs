using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Models
{
    [PublicAPI]
    public class ErrorModel
    {
        public string Key { get; set; }
        public string ErrorMessage { get; set; }
    }
}