using System.Runtime.CompilerServices;
using Argon;
using Enigmatry.Entry.Blueprint.Common.Tests;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.SmartEnums.VerifyTests;

namespace Enigmatry.Entry.Blueprint.Api.Tests;

public static class VerifierSettingsInitializer
{
    [ModuleInitializer]
    public static void Init() => CommonVerifierSettingsInitializer.Init();
}
