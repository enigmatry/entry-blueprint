using Enigmatry.Entry.Blueprint.Infrastructure.Api.Logging;
using Microsoft.Extensions.Configuration;
using Shouldly;

namespace Enigmatry.Entry.Blueprint.Api.Tests.CoreFeatures;

[Category("unit")]
public class SecurityLoggerFixture
{
    private string _logFilePath = null!;

    [SetUp]
    public void SetUp() =>
        _logFilePath = Path.Combine(TestContext.CurrentContext.WorkDirectory, $"security-{Guid.NewGuid():N}.log");

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(_logFilePath))
        {
            File.Delete(_logFilePath);
        }
    }

    [Test]
    public async Task EventsAreWrittenToTheSinkFromTheSecuritySerilogSectionWithSourceContext()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "SecuritySerilog:MinimumLevel:Default", "Debug" },
                { "SecuritySerilog:WriteTo:0:Name", "File" },
                { "SecuritySerilog:WriteTo:0:Args:path", _logFilePath },
                { "SecuritySerilog:WriteTo:0:Args:outputTemplate", "[{Level}] [{SourceContext}] {Message}{NewLine}" }
            })
            .Build();

        using (var serilogLogger = new SecuritySerilogLogger(configuration))
        {
            var logger = new SecurityLogger<SecurityLoggerFixture>(serilogLogger);
            logger.LogSecurityWarning("User {UserId} was denied access.", 42);
        }

        var content = await File.ReadAllTextAsync(_logFilePath);
        content.ShouldContain($"[Warning] [{typeof(SecurityLoggerFixture).FullName}] User 42 was denied access.");
    }

    [Test]
    public void WithoutSecuritySerilogSectionLoggingIsSilent()
    {
        var configuration = new ConfigurationBuilder().Build();

        using var serilogLogger = new SecuritySerilogLogger(configuration);
        var logger = new SecurityLogger<SecurityLoggerFixture>(serilogLogger);
        var act = () => logger.LogSecurityInformation("Nothing is persisted.");

        act.ShouldNotThrow();
        File.Exists(_logFilePath).ShouldBeFalse();
    }
}
