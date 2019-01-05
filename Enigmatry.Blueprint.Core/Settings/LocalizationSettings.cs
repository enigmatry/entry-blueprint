using System;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Core.Settings
{
    [UsedImplicitly]
    public class LocalizationSettings
    {
        public TimeSpan CacheDuration { get; set; }
        public string ResourcesPath { get; set; }
    }
}