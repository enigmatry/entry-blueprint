using Enigmatry.Blueprint.Core.Settings;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Enigmatry.Blueprint.Infrastructure.ApplicationInsights
{
    [UsedImplicitly]
    public class SettingsBasedTelemetryProcessor : ITelemetryProcessor
    {
        private readonly SettingsBasedTelemetryTypeFilteringRule _settingsBasedTelemetryTypeFilteringRule;
        private ITelemetryProcessor Next { get; }

        public SettingsBasedTelemetryProcessor(ITelemetryProcessor next, ApplicationInsightsSettings settings)
        {
            Next = next;
            _settingsBasedTelemetryTypeFilteringRule = new SettingsBasedTelemetryTypeFilteringRule(settings);
        }

        public void Process(ITelemetry item)
        {
            if (_settingsBasedTelemetryTypeFilteringRule.IsIncluded(item))
            {
                Next.Process(item);
            }
        }
    }
}
