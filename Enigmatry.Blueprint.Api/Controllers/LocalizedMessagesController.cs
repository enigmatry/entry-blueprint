using Enigmatry.Blueprint.Api.Localization;
using Enigmatry.Blueprint.Api.Models;
using Enigmatry.Blueprint.Api.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Enigmatry.Blueprint.Api.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class LocalizedMessagesController : Controller
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        public LocalizedMessagesController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet]
        public LocalizedMessagesModel Get()
        {
            LocalizedString message1 = _localizer["GoodMorning"];
            LocalizedString message2 = _localizer["GoodEvening"];

            return new LocalizedMessagesModel {Message1 = message1, Message2 = message2};
        }

        [HttpPost]
        public LocalizedMessagesPostModel Post(LocalizedMessagesPostModel model)
        {
            return model;
        }
    }
}