using Enigmatry.Blueprint.Data.Migrations.Seeding;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.Configuration;

public class TestConfigurationBuilder
{
    private string _dbContextName = String.Empty;

    public TestConfigurationBuilder WithDbContextName(string contextName)
    {
        _dbContextName = contextName;
        return this;
    }

    public IConfiguration Build()
    {
        EnsureParametersBeforeBuild();
        var configurationBuilder = new ConfigurationBuilder();

        var dict = new Dictionary<string, string?>
        {
            {"UseDeveloperExceptionPage", "true"},
            {"DbContext:SensitiveDataLoggingEnabled", "true"},
            {"DbContext:ConnectionResiliencyMaxRetryCount", "10"},
            {"DbContext:ConnectionResiliencyMaxRetryDelay", "0.00:00:30"},
            {
                $"ConnectionStrings:{_dbContextName}",
                GetConnectionString()
            },
            {"App:Smtp:UsePickupDirectory", "true"},
            {"App:Smtp:PickupDirectoryLocation", GetSmtpPickupDirectoryLocation()},
            {"ApplicationInsights:TelemetryProcessor:ExcludedTypes", String.Empty },
            {"HealthChecks:TokenAuthorizationEnabled", "false"}
        };

        configurationBuilder.AddInMemoryCollection(dict);
        return configurationBuilder.Build();
    }

    private static string GetSmtpPickupDirectoryLocation() => TestContext.CurrentContext.TestDirectory;

    private void EnsureParametersBeforeBuild()
    {
        if (String.IsNullOrWhiteSpace(_dbContextName))
        {
            throw new InvalidOperationException("Missing db context name");
        }
    }

    private static string GetConnectionString()
    {
        var connectionString = Environment.GetEnvironmentVariable("IntegrationTestsConnectionString");
        if (!String.IsNullOrEmpty(connectionString))
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            WriteLine("Connection string read from environment variable.");
            WriteLine($"Integration Tests using database: {builder.DataSource}");
            return connectionString;
        }

        return DevelopmentConnectionsStrings.IntegrationTestsConnectionString;
    }

    private static void WriteLine(string message) =>
        TestContext.WriteLine(message);
}
