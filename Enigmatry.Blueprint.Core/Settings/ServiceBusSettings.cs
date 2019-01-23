namespace Enigmatry.Blueprint.Core.Settings
{
    public class ServiceBusSettings
    {
        public bool AzureServiceBusEnabled { get; set; }
        public string SubscriptionClientName { get; set; }
        public string EventBusConnection { get;set; }
    }
}