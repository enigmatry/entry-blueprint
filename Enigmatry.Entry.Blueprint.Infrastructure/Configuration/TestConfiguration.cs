using Microsoft.Extensions.Configuration;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Configuration;

// Workaround for the tests, issue is mentioned here: https://github.com/dotnet/aspnetcore/issues/37680
internal static class TestConfiguration
{
    // This async local is set in from tests and it flows to main
    private static readonly AsyncLocal<Action<IConfigurationBuilder>?> Current = new();

    /// <summary>
    /// Adds the current test configuration to the application in the "right" place
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder</param>
    /// <returns>The modified <see cref="IConfigurationBuilder"/></returns>
    public static IConfigurationBuilder AddTestConfiguration(this IConfigurationBuilder configurationBuilder)
    {
        if (Current.Value is { } configure)
        {
            configure(configurationBuilder);
        }

        return configurationBuilder;
    }

    /// <summary>
    /// Unit tests can use this to flow state to the main program and change configuration
    /// </summary>
    /// <param name="action"></param>
    public static void Create(Action<IConfigurationBuilder> action) => Current.Value = action;
}
