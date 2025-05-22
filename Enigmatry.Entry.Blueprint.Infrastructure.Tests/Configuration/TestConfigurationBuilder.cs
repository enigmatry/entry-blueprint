using Microsoft.Extensions.Configuration;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Tests.Configuration;

public class TestConfigurationBuilder
{
    private string _dbContextName = String.Empty;
    private string _connectionString = String.Empty;
    private Action<ConfigurationBuilder>? _extraConfiguration;

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
            { "UseDeveloperExceptionPage", "true" },
            { "DbContext:SensitiveDataLoggingEnabled", "true" },
            { "DbContext:UseAccessToken", "false" },
            { "DbContext:ConnectionResiliencyMaxRetryCount", "10" },
            { "DbContext:ConnectionResiliencyMaxRetryDelay", "0.00:00:30" },
            { "DbContext:RegisterMigrationsAssembly", "true" },
            { $"ConnectionStrings:{_dbContextName}", _connectionString },
            { "App:Smtp:UsePickupDirectory", "true" },
            { "App:Smtp:PickupDirectoryLocation", GetSmtpPickupDirectoryLocation() },
            { "App:AzureAd:Instance", "https://enigmatryb2cdev.b2clogin.com" },
            { "App:AzureAd:ClientId", "a8793ce9-86dc-4d7e-aa70-361a3c5a5150" },
            { "App:AzureAd:Domain", "enigmatryb2cdev.onmicrosoft.com" },
            { "App:AzureAd:SignUpSignInPolicyId", "B2C_1_entry_blueprint_sign_in" },
            { "HealthChecks:TokenAuthorizationEnabled", "false" },
            { "KeyVault:Enabled", "false" }
        };

        configurationBuilder.AddInMemoryCollection(dict);
        _extraConfiguration?.Invoke(configurationBuilder);
        return configurationBuilder.Build();
    }

    public TestConfigurationBuilder WithSchedulerConfiguration()
    {
        _extraConfiguration = configurationBuilder =>
        {
            var dict = new Dictionary<string, string?>
            {
                { "Scheduling:Host:quartz.scheduler.instanceName", "Enigmatry.Entry.Scheduler" },
                { "Scheduling:Jobs:CleanOldProductsJob:Cronex", "0 0 0 * * ?" }
            };

            configurationBuilder.AddInMemoryCollection(dict);
        };
        return this;
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
