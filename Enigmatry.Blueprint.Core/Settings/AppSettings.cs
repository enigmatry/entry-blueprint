﻿using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Core.Settings
{
    [UsedImplicitly]
    public class AppSettings
    {
        public ServiceBusSettings ServiceBus { get; set; }
        public GitHubApiSettings GitHubApi { get; set; }
        public SmtpSettings Smtp { get; set; }
    }
}
