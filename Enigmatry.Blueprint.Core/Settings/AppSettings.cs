using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Core.Settings
{
    [UsedImplicitly]
    public class AppSettings
    {
        [UsedImplicitly]
        public SmtpSettings Smtp { get; set; } = new SmtpSettings();
    }
}
