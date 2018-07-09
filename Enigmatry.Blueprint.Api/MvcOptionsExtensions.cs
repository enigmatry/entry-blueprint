using Microsoft.AspNetCore.Mvc;

namespace Enigmatry.Blueprint.Api
{
    public static class MvcOptionsExtensions
    {
        public static MvcOptions DefaultConfigure(this MvcOptions options)
        {
            return options;
        }
    }
}