using System;
using System.Collections.Generic;
using Enigmatry.Blueprint.Core.Settings;
using Microsoft.ApplicationInsights.Channel;
using Serilog.Events;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights.TelemetryConverters;

namespace Enigmatry.Blueprint.Infrastructure.ApplicationInsights
{
    public class SettingsBasedTraceTelemetryConverter : TraceTelemetryConverter
    {
        private readonly SettingsBasedTelemetryTypeFilteringRule _settingsBasedTelemetryTypeFilteringRule;

        public SettingsBasedTraceTelemetryConverter(ApplicationInsightsSettings settings)
        {
            _settingsBasedTelemetryTypeFilteringRule = new SettingsBasedTelemetryTypeFilteringRule(settings);
        }

        public override IEnumerable<ITelemetry> Convert(LogEvent logEvent, IFormatProvider formatProvider)
        {
            foreach (ITelemetry telemetry in base.Convert(logEvent, formatProvider))
            {
                if (_settingsBasedTelemetryTypeFilteringRule.IsIncluded(telemetry))
                {
                    yield return telemetry;
                }
            }
        }
    }
}
