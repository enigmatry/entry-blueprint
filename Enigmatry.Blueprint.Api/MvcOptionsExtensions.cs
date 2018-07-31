using Enigmatry.Blueprint.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Api
{
    public static class MvcOptionsExtensions
    {
        public static MvcOptions DefaultConfigure(this MvcOptions options, IConfiguration configuration,
            ILoggerFactory loggerFactory)
        {
            options.Filters.Add(new CancelSavingTransactionAttribute());
            options.Filters.Add(new HandleExceptionsFilter(configuration.UseDeveloperExceptionPage(),
                loggerFactory.CreateLogger<HandleExceptionsFilter>()));
            return options;
        }
    }
}