using System.Runtime.CompilerServices;
using Argon;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.SmartEnums.VerifyTests;

namespace Enigmatry.Entry.Blueprint.Common.Tests;

public static class CommonVerifierSettingsInitializer
{
    public static void Init()
    {
        VerifierSettings.IncludeObsoletes();
        VerifierSettings.AddExtraSettings(settings =>
        {
            settings.Converters.EntryAddSmartEnumJsonConverters([AssemblyFinder.DomainAssembly]);
            settings.NullValueHandling = NullValueHandling.Ignore;

            // primarily for default enum values
            settings.DefaultValueHandling = DefaultValueHandling.Include;
        });
    }
}
