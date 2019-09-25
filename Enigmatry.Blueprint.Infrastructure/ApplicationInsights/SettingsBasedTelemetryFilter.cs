using System.Collections.Generic;
using System.Linq;
using Enigmatry.Blueprint.Core.Settings;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Enigmatry.Blueprint.Infrastructure.ApplicationInsights
{
    [UsedImplicitly]
    public class SettingsBasedTelemetryFilter : ITelemetryProcessor
    {
        private readonly Dictionary<string, string> _excludedTypes;
        private ITelemetryProcessor Next { get; }

        // Link processors to each other in a chain.
        public SettingsBasedTelemetryFilter(ITelemetryProcessor next, ApplicationInsightsSettings settings)
        {
            Next = next;
            _excludedTypes = PrepareIncludedTypes(settings.TelemetryProcessor.ExcludedTypes);
        }

        public void Process(ITelemetry item)
        {
            // To filter out an item, just return
            if (IsTypeExcluded(item))
            {
                return;
            }

            Next.Process(item);
        }

        // Example: replace with your own criteria.
        private bool IsTypeExcluded(ITelemetry item)
        {
            // all implementors of ITelemetryItem contain "Telemetry" in the name
            // e.g RequestTelemetry, DependencyTelemetry, etc.
            var typeShortName = item.GetType().Name;

            return _excludedTypes.ContainsKey(typeShortName);
        }

        private static Dictionary<string, string> PrepareIncludedTypes(string includedTypes)
        {
            // suffix with telemetry so that we can find the type
            return includedTypes
                .Split(';')
                .ToDictionary(x => x + "Telemetry", x => x.ToLowerInvariant());
        }
    }
}
