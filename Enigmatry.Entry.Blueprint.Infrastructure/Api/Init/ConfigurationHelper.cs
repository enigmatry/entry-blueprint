using Microsoft.Extensions.Configuration;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;

public static class ConfigurationHelper
{
    // needed because of early reading of configuration file in Program.cs (e.g. for Serilog or KeyVault), before WebHostBuilder has been built.
    // Later new configuration is built again.
    public static IConfiguration CreateConfiguration(string environmentParameterPrefix = "ASPNETCORE_") =>
        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable($"{environmentParameterPrefix}ENVIRONMENT") ?? "Production"}.json",
                true)
            .Build();
}
