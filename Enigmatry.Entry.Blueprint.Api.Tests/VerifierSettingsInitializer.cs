using System.Runtime.CompilerServices;
using Enigmatry.Entry.Blueprint.Common.Tests;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;

namespace Enigmatry.Entry.Blueprint.Api.Tests;

public static class VerifierSettingsInitializer
{
    [ModuleInitializer]
    public static void Init() => CommonVerifierSettingsInitializer.Init(AssemblyFinder.ApiAssembly);
}
