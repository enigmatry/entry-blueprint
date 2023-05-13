using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Enigmatry.Blueprint.Api.Features.CkeditorDemo
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class CkeditorDemoController : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public GetDescription.Response Get()
        {
            return new GetDescription.Response();
        }
    }
}
