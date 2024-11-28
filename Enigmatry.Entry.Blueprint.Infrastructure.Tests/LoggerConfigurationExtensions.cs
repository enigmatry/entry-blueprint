using System.Globalization;
using Serilog;
using Serilog.Events;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Tests;

public static class LoggerConfigurationExtensions
{
    public static void ConfigureSerilogForIntegrationTests(this LoggerConfiguration loggerConfiguration) =>

        // this allows serilog log statements to appear in the test console runner
        // e.g. database sql queries (issued by DbContext) can be observed this way in the test
        loggerConfiguration.WriteTo.Console(
                LogEventLevel.Information,
                formatProvider: CultureInfo.InvariantCulture,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")

            // suppressing some log events that are not relevant for the tests (reduces noise)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Model.Validation", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware", LogEventLevel.Error)
            .MinimumLevel.Override("Enigmatry.Entry.MediatR.LoggingBehavior", LogEventLevel.Error);
}

