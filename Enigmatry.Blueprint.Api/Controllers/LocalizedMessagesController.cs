using System;
using Enigmatry.Blueprint.Api.Localization;
using Enigmatry.Blueprint.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Api.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class LocalizedMessagesController : Controller
    {
        private readonly IStringLocalizer<AdditionalResource> _additionalResourceLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;
        private readonly ILogger<LocalizedMessagesController> _logger;

        public LocalizedMessagesController(IStringLocalizer<SharedResource> sharedResourceLocalizer, IStringLocalizer<AdditionalResource> additionalResourceLocalizer, ILogger<LocalizedMessagesController> logger)
        {
            _sharedResourceLocalizer = sharedResourceLocalizer;
            _additionalResourceLocalizer = additionalResourceLocalizer;
            _logger = logger;
        }

        [HttpGet]
        public LocalizedMessagesModel Get()
        {
            string goodMorning = _sharedResourceLocalizer["GoodMorning"];
            string goodEvening = _sharedResourceLocalizer["GoodEvening"];
            string message3 = _additionalResourceLocalizer["AdditionalResourceMessage"];

            _logger.LogInformation("Returning localized messages: {GoodMorning} and {GoodEvening}", goodMorning, goodEvening);

            return new LocalizedMessagesModel {Message1 = goodMorning, Message2 = goodEvening, Message3 = message3};
        }

        [HttpGet]
        [Route("exception")]
        public LocalizedMessagesModel GetException()
        {
            throw new Exception("Exception from test");
        }

        [HttpPost]
        public LocalizedMessagesPostModel Post(LocalizedMessagesPostModel model)
        {
            return model;
        }
    }
}
