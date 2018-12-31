using System;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Core.Settings
{
    [UsedImplicitly]
    public class GitHubApiSettings
    {
        public string BaseUrl { get; set; }
        public TimeSpan Timeout { get; set; }
    }
}