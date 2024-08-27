using System.Reflection;
using Argon;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.SmartEnums.VerifyTests;

namespace Enigmatry.Entry.Blueprint.Common.Tests;

public static class CommonVerifierSettingsInitializer
{
    public static void Init(Assembly entryAssembly)
    {
        // properties marked [Obsolete] will also appear in the verified output
        VerifierSettings.IncludeObsoletes();

        VerifierSettings.AddExtraSettings(settings =>
        {
            // needed for SmartEnum serialization 
            settings.Converters.EntryAddSmartEnumJsonConverters([entryAssembly, AssemblyFinder.DomainAssembly]);

            settings.NullValueHandling = NullValueHandling.Ignore;

            // primarily for default enum values to appear in the verified output
            settings.DefaultValueHandling = DefaultValueHandling.Include;
        });
    }
}
