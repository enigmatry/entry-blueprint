using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Core.Settings
{
    [UsedImplicitly]
    public class ApplicationInsightsSettings
    {
        public AdaptiveSamplingSettings AdaptiveSampling { get; set; }

        public TelemetryProcessorSettings TelemetryProcessor { get; set; }
        
        [UsedImplicitly]
        public class AdaptiveSamplingSettings
        {
            public string ExcludedTypes { get; set; }
            public string IncludedTypes { get; set; }
            public int MaxTelemetryItemsPerSecond { get; set; }
        }

        [UsedImplicitly]
        public class TelemetryProcessorSettings
        {
            public string ExcludedTypes { get; set; }
        }
    }
}
