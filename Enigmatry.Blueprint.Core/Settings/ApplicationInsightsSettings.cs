using JetBrains.Annotations;
using Serilog.Events;

namespace Enigmatry.Blueprint.Core.Settings
{
    [UsedImplicitly]
    public class ApplicationInsightsSettings
    {
        public string InstrumentationKey { get; set; }
        public TelemetryProcessorSettings TelemetryProcessor { get; set; }
        public LogEventLevel SerilogLogsRestrictedToMinimumLevel { get; set; }

        [UsedImplicitly]
        public class TelemetryProcessorSettings
        {
            public string ExcludedTypes { get; set; }
        }
    }
}
