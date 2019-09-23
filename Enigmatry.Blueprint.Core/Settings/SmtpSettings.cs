using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Core.Settings
{
    [UsedImplicitly]
    public class SmtpSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool UsePickupDirectory { get; set; }
        public string PickupDirectoryLocation { get; set; }
        public string From { get; set; }
    }
}
