using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Core.Settings
{
    [UsedImplicitly]
    public class ApplicationInsightsSettings
    {
        public TelemetryProcessorSettings TelemetryProcessor { get; set; }

        [UsedImplicitly]
        public class TelemetryProcessorSettings
        {
            public string ExcludedTypes { get; set; }
        }
    }
}
