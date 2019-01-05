using Enigmatry.Blueprint.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Enigmatry.Blueprint.Api.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IStringLocalizer _localizer;

        public HomeController(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        [HttpGet]
        public HomeModel Get()
        {
            LocalizedString message1 = _localizer["good-morning"];
            LocalizedString message2 = _localizer["good-evening"];

            return new HomeModel {Message1 = message1, Message2 = message2};
        }
    }
}