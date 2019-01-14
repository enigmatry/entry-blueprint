using System.Net.Http;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Api.Models;
using Enigmatry.Blueprint.Api.Tests.Common;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Api; 
using FluentAssertions;
using NUnit.Framework;

namespace Enigmatry.Blueprint.Api.Tests
{
    [Category("integration")]
    public class LocalizedMessagesControllerFixture : IntegrationFixtureBase
    {
        [Test]
        public async Task TestGetDefaultLocalizedMessage()
        {
            LocalizedMessagesModel result = await Client.GetAsync<LocalizedMessagesModel>("localizedmessages");
            result.Message1.Should().Be("Good morning");
            result.Message2.Should().Be("Good evening");
            result.Message3.Should().Be("Additional Resource Message EN");
        }

        [Test]
        public async Task TestGetDutchLocalizedMessage()
        {
            LocalizedMessagesModel result = await Client.GetAsync<LocalizedMessagesModel>("localizedmessages?culture=nl-NL");
            result.Message1.Should().Be("Goedemorgen");
            result.Message2.Should().Be("Goedenavond");
            result.Message3.Should().Be("Additional Resource Message NL");
        }

        [Test]
        public async Task TestValidationMessagesInDefaultLanguage()
        {
            var model = new LocalizedMessagesPostModel {Email = "mail"};

            HttpResponseMessage response = await Client.PostAsJsonAsync("localizedmessages", model);

            response.Should().BeBadRequest().And.ContainValidationError("id", "'Id' must be greater than '0'.");
            response.Should().ContainValidationError("name", "'Name' must not be empty.");
            response.Should().ContainValidationError("name", "'Name' must be unique.");
            response.Should().ContainValidationError("email", "'Email' is not a valid email address.");
            response.Should().ContainValidationError("email", "The length of 'Email' must be 2 characters or fewer. You entered 4 characters.");
        }

        [Test]
        public async Task TestValidationMessagesInDutchLanguage()
        {
            var model = new LocalizedMessagesPostModel {Email = "mail"};

            HttpResponseMessage response = await Client.PostAsJsonAsync("localizedmessages?culture=nl-NL", model);

            response.Should().BeBadRequest().And.ContainValidationError("id", "'Id' moet groter zijn dan '0'.");
            response.Should().ContainValidationError("name", "'Naam' mag niet leeg zijn.");
            response.Should().ContainValidationError("name", "'Naam' moet uniek zijn.");
            response.Should().ContainValidationError("email", "'Email' is geen geldig email adres.");
            response.Should().ContainValidationError("email", "De lengte van 'Email' moet kleiner zijn dan of gelijk aan 2 tekens. U hebt 4 -tekens ingevoerd.");
        }
    }
}