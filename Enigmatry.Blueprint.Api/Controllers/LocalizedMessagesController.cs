using Enigmatry.Blueprint.Api.Localization;
using Enigmatry.Blueprint.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Enigmatry.Blueprint.Api.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class LocalizedMessagesController : Controller
    {
        private readonly IStringLocalizer<AdditionalResource> _additionalResourceLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;

        public LocalizedMessagesController(IStringLocalizer<SharedResource> sharedResourceLocalizer, IStringLocalizer<AdditionalResource> additionalResourceLocalizer)
        {
            _sharedResourceLocalizer = sharedResourceLocalizer;
            _additionalResourceLocalizer = additionalResourceLocalizer;
        }

        [HttpGet]
        public LocalizedMessagesModel Get()
        {
            string message1 = _sharedResourceLocalizer["GoodMorning"];
            string message2 = _sharedResourceLocalizer["GoodEvening"];
            string message3 = _additionalResourceLocalizer["AdditionalResourceMessage"];

            return new LocalizedMessagesModel {Message1 = message1, Message2 = message2, Message3 = message3};
        }

        [HttpPost]
        public LocalizedMessagesPostModel Post(LocalizedMessagesPostModel model)
        {
            return model;
        }
    }
}