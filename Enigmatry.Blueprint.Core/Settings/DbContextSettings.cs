using System;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Core.Settings
{
    [UsedImplicitly]
    public class DbContextSettings
    {
        public bool SensitiveDataLoggingEnabled { get; set; }

        public int ConnectionResiliencyMaxRetryCount { get; set; }

        public TimeSpan ConnectionResiliencyMaxRetryDelay { get; set; }
    }
}