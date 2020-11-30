using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Enigmatry.Blueprint.Api
{
    public static class MappingExtensions
    {
        public static ActionResult<TDestination> MapToActionResult<TDestination>(this IMapper mapper, object value)
        {
            return value == null ? new NotFoundResult() : (ActionResult<TDestination>)new OkObjectResult(mapper.Map<TDestination>(value));
        }
    }
}
