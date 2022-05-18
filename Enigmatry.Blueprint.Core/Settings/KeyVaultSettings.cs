using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Core.Settings
{
    [UsedImplicitly]
    public class KeyVaultSettings
    {
        public bool Enabled { get; set; }
        public string Name { get; set; } = String.Empty;
    }
}
