using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Configuration;

[UsedImplicitly]
public class ApplicationInsightsSettings : Entry.Core.Settings.ApplicationInsightsSettings
{
    public string ConnectionString { get; set; } = String.Empty;
}
