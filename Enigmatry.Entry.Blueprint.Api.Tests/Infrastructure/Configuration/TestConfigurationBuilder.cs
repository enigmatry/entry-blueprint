using Microsoft.Extensions.Configuration;

namespace Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Configuration;

public class TestConfigurationBuilder
{
    private string _dbContextName = String.Empty;
    private string _connectionString = String.Empty;

    public TestConfigurationBuilder WithDbContextName(string contextName)
    {
        _dbContextName = contextName;
        return this;
    }

    public TestConfigurationBuilder WithConnectionString(string connectionString)
    {
        _connectionString = connectionString;
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
            {"DbContext:UseAccessToken", "false"},
            {"DbContext:ConnectionResiliencyMaxRetryCount", "10"},
            {"DbContext:ConnectionResiliencyMaxRetryDelay", "0.00:00:30"},
            {
                $"ConnectionStrings:{_dbContextName}", _connectionString
            },
            {"App:Smtp:UsePickupDirectory", "true"},
            {"App:Smtp:PickupDirectoryLocation", GetSmtpPickupDirectoryLocation()},
            {"App:AzureAd:Instance", "https://enigmatryb2cdev.b2clogin.com"},
            {"App:AzureAd:ClientId", "a8793ce9-86dc-4d7e-aa70-361a3c5a5150"},
            {"App:AzureAd:Domain", "enigmatryb2cdev.onmicrosoft.com"},
            {"App:AzureAd:SignUpSignInPolicyId", "B2C_1_entry_blueprint_sign_in"},
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
}
