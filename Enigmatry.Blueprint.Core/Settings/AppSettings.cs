using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Core.Settings
{
    [UsedImplicitly]
    public class AppSettings
    {
        [UsedImplicitly]
        public ServiceBusSettings ServiceBus { get; set; } = new ServiceBusSettings();

        [UsedImplicitly]
        public GitHubApiSettings GitHubApi { get; set; } = new GitHubApiSettings();

        [UsedImplicitly]
        public SmtpSettings Smtp { get; set; } = new SmtpSettings();
    }
}
