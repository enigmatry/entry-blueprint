using System;

namespace Enigmatry.Blueprint.Core.Settings
{
    public class ServiceBusSettings
    {
        public bool AzureServiceBusEnabled { get; set; }
        public string SubscriptionClientName { get; set; } = String.Empty;
        public string EventBusConnection { get;set; } = String.Empty;
    }
}
