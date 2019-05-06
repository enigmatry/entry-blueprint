using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.Configuration
{
    public class TestConfigurationBuilder
    {
        private string _dbContextName;

        public TestConfigurationBuilder WithDbContextName(string contextName)
        {
            _dbContextName = contextName;
            return this;
        }

        public IConfiguration Build()
        {
            EnsureParametersBeforeBuild();
            var configurationBuilder = new ConfigurationBuilder();

            var dict = new Dictionary<string, string>
            {
                {"UseDeveloperExceptionPage", "true"},
                {"DbContext:SensitiveDataLoggingEnabled", "true"},
                {"DbContext:ConnectionResiliencyMaxRetryCount", "10"},
                {"DbContext:ConnectionResiliencyMaxRetryDelay", "0.00:00:30"},
                {
                    $"ConnectionStrings:{_dbContextName}",
                    GetConnectionString()
                },
                {"App:ServiceBus:AzureServiceBusEnabled", "false"},
                {"App:Localization:CacheDuration", "0:00:00:30"},
                {"App:GitHubApi:BaseUrl", "https://api.github.com"},
                {"App:GitHubApi:Timeout", "0.00:00:15"},
                {"App:Smtp:UsePickupDirectory", "true"},
                {"App:Smtp:PickupDirectoryLocation", GetSmtpPickupDirectoryLocation()}
            };

            configurationBuilder.AddInMemoryCollection(dict);
            return configurationBuilder.Build();
        }

        public static string GetSmtpPickupDirectoryLocation() => TestContext.CurrentContext.TestDirectory;


        private void EnsureParametersBeforeBuild()
        {
            if (string.IsNullOrWhiteSpace(_dbContextName))
            {
                throw new InvalidOperationException("Missing db context name");
            }
        }

        private static string GetConnectionString()
        {
            string connectionString = Environment.GetEnvironmentVariable("IntegrationTestsConnectionString");
            if (!string.IsNullOrEmpty(connectionString))
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                WriteLine("Connection string read from environment variable.");
                WriteLine($"Integration Tests using database: {builder.DataSource}");
                return connectionString;
            }

            const string dbName = "TD_1.0_integration_testing";
            WriteLine($"Integration Tests using database: {dbName}");
            return $"Server=.;Database={dbName};Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        private static void WriteLine(string message) => 
            TestContext.WriteLine(message);
    }
}
