using Microsoft.Extensions.Configuration;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;

public static class ConfigurationHelper
{
    // needed because of early reading of configuration file in Program.cs (e.g. for Serilog or KeyVault), before WebHostBuilder has been built.
    // Later new configuration is built again.
    public static IConfiguration CreateConfiguration(IEnumerable<string> args, string environmentParameterPrefix = "ASPNETCORE_")
    {
        if (args.Any(a => a.Contains("IsTest", StringComparison.InvariantCultureIgnoreCase)))
        {
            return new ConfigurationBuilder().Build();
        }
        var environment = Environment.GetEnvironmentVariable($"{environmentParameterPrefix}ENVIRONMENT");
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile(
                $"appsettings.{environment ?? "Production"}.json",
                true)
            .Build();
    }
}
