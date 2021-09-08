using System;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Core.Settings
{
    [UsedImplicitly]
    public class HealthCheckSettings
    {
        public int MaximumMemoryMegaBytes { get; set; }
        public string RequiredToken { get; set; } = String.Empty;
    }
}
