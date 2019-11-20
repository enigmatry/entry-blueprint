using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Enigmatry.Blueprint.Api
{
    public static class MappingExtensions
    {
        public static ActionResult<TDestination> MapToActionResult<TDestination>(this IMapper mapper, object value)
        {
            if (value == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(mapper.Map<TDestination>(value));
        }
    }
}
