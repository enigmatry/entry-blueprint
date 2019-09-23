using Enigmatry.Blueprint.Core.Settings;
using Microsoft.ApplicationInsights.Extensibility;

namespace Enigmatry.Blueprint.Infrastructure.ApplicationInsights
{
    public static class TelemetryConfigurationExtensions
    {
        public static void ConfigureTelemetry(this TelemetryConfiguration configuration, ApplicationInsightsSettings settings)
        {
            var telemetryBuilder = configuration.DefaultTelemetrySink.TelemetryProcessorChainBuilder;
            
            telemetryBuilder.Use(next => new SuccessfulDependencyFilter(next, settings.TelemetryProcessor));

            var adaptiveSamplingSettings = settings.AdaptiveSampling;
            if (adaptiveSamplingSettings != null)
            {
                telemetryBuilder.UseAdaptiveSampling(
                    maxTelemetryItemsPerSecond: adaptiveSamplingSettings.MaxTelemetryItemsPerSecond,
                    excludedTypes: adaptiveSamplingSettings.ExcludedTypes, 
                    includedTypes:adaptiveSamplingSettings.IncludedTypes);
            }
            else
            {
                //default sampling
                telemetryBuilder.UseAdaptiveSampling();
            }
            
            telemetryBuilder.Build();
        }
    }
}
