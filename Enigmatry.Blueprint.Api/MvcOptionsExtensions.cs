using Enigmatry.Blueprint.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Api
{
    public static class MvcOptionsExtensions
    {
        public static MvcOptions DefaultConfigure(this MvcOptions options, IConfiguration configuration)
        {
            options.Filters.Add(new CancelSavingTransactionAttribute());
            options.Filters.Add(new HandleExceptionsFilter(configuration.UseDeveloperExceptionPage()));
            return options;
        }
    }
}
