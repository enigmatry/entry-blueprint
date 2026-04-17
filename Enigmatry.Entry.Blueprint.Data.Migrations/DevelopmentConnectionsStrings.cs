namespace Enigmatry.Entry.Blueprint.Data.Migrations;

public static class DevelopmentConnectionsStrings
{
#pragma warning disable IDE0032 // Use auto property
    private static readonly string DatabaseName = "Enigmatry.Entry.Blueprint".Replace(".", "-", StringComparison.InvariantCulture).ToLowerInvariant();
#pragma warning restore IDE0032 // Use auto property
    public static string MainConnectionString => $"Server=.;Database={DatabaseName};Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
}
