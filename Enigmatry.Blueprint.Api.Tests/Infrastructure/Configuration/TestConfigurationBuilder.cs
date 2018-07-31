using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.Configuration
{
    public class TestConfigurationBuilder
    {
        private string _connectionString;
        private string _dbContextName;

        public TestConfigurationBuilder WithConnectionString(string connectionString)
        {
            _connectionString = connectionString;
            return this;
        }

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
                    _connectionString
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

            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new InvalidOperationException("Missing connection string");
            }
        }
    }
}