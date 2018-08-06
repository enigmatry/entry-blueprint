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
                {
                    $"ConnectionStrings:{_dbContextName}",
                    ReadConnectionString()
                },
                {"App:ServiceBus:AzureServiceBusEnabled", "false"}
            };

            configurationBuilder.AddInMemoryCollection(dict);
            return configurationBuilder.Build();
        }

        private void EnsureParametersBeforeBuild()
        {
            if (string.IsNullOrWhiteSpace(_dbContextName))
            {
                throw new InvalidOperationException("Missing db context name");
            }
        }

        private static string ReadConnectionString()
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
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