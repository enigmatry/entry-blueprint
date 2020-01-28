using Microsoft.AspNetCore.Mvc;

namespace Enigmatry.Blueprint.Api
{
    public static class ActionResultExtensions
    {
        public static ActionResult<TDestination> ToActionResult<TDestination>(this TDestination? model) where TDestination : class
        {
            return model == null ? 
                (ActionResult<TDestination>)new NotFoundResult() 
                : new OkObjectResult(model);
        }
    }
}
