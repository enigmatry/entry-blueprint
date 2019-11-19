using JetBrains.Annotations;
using Serilog.Events;

namespace Enigmatry.Blueprint.Core.Settings
{
    [UsedImplicitly]
    public class ApplicationInsightsSettings
    {
        public string InstrumentationKey { get; set; }
        public LogEventLevel SerilogLogsRestrictedToMinimumLevel { get; set; }
    }
}
