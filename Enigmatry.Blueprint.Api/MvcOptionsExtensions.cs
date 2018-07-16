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
            options.Filters.Add(new CancelSavingAttribute());
            options.Filters.Add(new ExceptionAsJsonFilter(configuration.UseDeveloperExceptionPage(),
                loggerFactory.CreateLogger<ExceptionAsJsonFilter>()));
            return options;
        }
    }
}