using System.Collections.Generic;
using System.Linq;
using Enigmatry.Blueprint.Core.Settings;
using Microsoft.ApplicationInsights.Channel;

namespace Enigmatry.Blueprint.Infrastructure.ApplicationInsights
{
    public class SettingsBasedTelemetryTypeFilteringRule
    {
        private readonly Dictionary<string, string> _excludedTypes;

        public SettingsBasedTelemetryTypeFilteringRule(ApplicationInsightsSettings settings)
        {
            _excludedTypes = PrepareExcludedTypes(settings.TelemetryProcessor.ExcludedTypes);
        }

        public bool IsIncluded(ITelemetry item)
        {
            return !IsExcluded(item);
        }

        private bool IsExcluded(ITelemetry item)
        {
            // all implementors of ITelemetryItem contain "Telemetry" in the name
            // e.g RequestTelemetry, DependencyTelemetry, etc.
            var typeShortName = item.GetType().Name;

            return _excludedTypes.ContainsKey(typeShortName);
        }

        private static Dictionary<string, string> PrepareExcludedTypes(string includedTypes)
        {
            // suffix with telemetry so that we can find the type
            return includedTypes
                .Split(';')
                .ToDictionary(x => x + "Telemetry", x => x.ToLowerInvariant());
        }
    }
}
