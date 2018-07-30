using System;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Enigmatry.BuildingBlocks.EventBusServiceBus
{
    public class DefaultServiceBusPersisterConnection : IServiceBusPersisterConnection
    {
        private readonly ILogger<DefaultServiceBusPersisterConnection> _logger;

        private bool _disposed;
        private ITopicClient _topicClient;

        public DefaultServiceBusPersisterConnection(ServiceBusConnectionStringBuilder serviceBusConnectionStringBuilder,
            ILogger<DefaultServiceBusPersisterConnection> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            ServiceBusConnectionStringBuilder = serviceBusConnectionStringBuilder ??
                                                throw new ArgumentNullException(
                                                    nameof(serviceBusConnectionStringBuilder));
            _topicClient = new TopicClient(ServiceBusConnectionStringBuilder, RetryPolicy.Default);
        }

        public ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder { get; }

        public ITopicClient CreateModel()
        {
            if (_topicClient.IsClosedOrClosing)
            {
                _topicClient = new TopicClient(ServiceBusConnectionStringBuilder, RetryPolicy.Default);
            }

            return _topicClient;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
        }
    }
}