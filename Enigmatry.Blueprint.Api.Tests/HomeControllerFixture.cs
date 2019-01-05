using System.Threading.Tasks;
using Enigmatry.Blueprint.Api.Models;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Api; 
using FluentAssertions;
using NUnit.Framework;

namespace Enigmatry.Blueprint.Api.Tests
{
    [Category("integration")]
    public class HomeControllerFixture : IntegrationFixtureBase
    {
        [Test]
        public async Task TestGetDefaultLocalizedMessage()
        {
            HomeModel result = await Client.GetAsync<HomeModel>("home");
            result.Message1.Should().Be("Good morning");
            result.Message2.Should().Be("Good evening");
        }

        [Test]
        public async Task TestGetDutchLocalizedMessage()
        {
            HomeModel result = await Client.GetAsync<HomeModel>("home?culture=nl-NL");
            result.Message1.Should().Be("Goedemorgen");
            result.Message2.Should().Be("Goedenavond");
        }
    }
}