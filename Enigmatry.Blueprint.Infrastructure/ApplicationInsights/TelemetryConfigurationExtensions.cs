using Enigmatry.Blueprint.Core.Settings;
using Microsoft.ApplicationInsights.Extensibility;

namespace Enigmatry.Blueprint.Infrastructure.ApplicationInsights
{
    public static class TelemetryConfigurationExtensions
    {
        public static void ConfigureTelemetry(this TelemetryConfiguration configuration, ApplicationInsightsSettings settings)
        {
            var telemetryBuilder = configuration.DefaultTelemetrySink.TelemetryProcessorChainBuilder;
            telemetryBuilder.UseAdaptiveSampling();
        }
    }
}
