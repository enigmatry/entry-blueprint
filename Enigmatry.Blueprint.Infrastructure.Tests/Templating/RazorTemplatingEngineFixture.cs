using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Infrastructure.Templating;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace Enigmatry.Blueprint.Infrastructure.Tests.Templating
{
    [Category("unit")]
    public class RazorTemplatingEngineFixture
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private RazorTemplatingEngine _templatingEngine;
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        [SetUp]
        public void Setup()
        {
            IHost host = BuildHost();
            IServiceScopeFactory scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
            IServiceScope serviceScope = scopeFactory.CreateScope();
            _templatingEngine = serviceScope.ServiceProvider.GetRequiredService<RazorTemplatingEngine>();
        }

        [Test]
        public async Task TestRenderFromFile()
        {
            string result = await _templatingEngine.RenderFromFileAsync("~/Templating/Sample.cshtml",
                new EmailModel {SampleText = "Hello world!"});

            result.Should().Contain("Hello world!");
            result.Should().Contain("Congratulations!");
        }

        private static IHost BuildHost()
        {
            IConfiguration configuration = BuildConfiguration();
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<RazorSampleConsoleStartup>()
                        .UseConfiguration(configuration);
                }).Build();
        }

        private static IConfiguration BuildConfiguration()
        {
            var rootDirectory = Directory.GetCurrentDirectory();
            var configValues = new Dictionary<string, string> {{"App:RazorViewsRootDirectory", rootDirectory}};

            return new ConfigurationBuilder()
                .AddInMemoryCollection(configValues)
                .Build();
        }
    }
}
