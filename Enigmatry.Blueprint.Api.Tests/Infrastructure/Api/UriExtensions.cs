using System;
using System.Collections.Generic;
using System.Linq;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.Api
{
    public static class UriExtensions
    {
        public static Uri AppendParameters(this Uri uri, KeyValuePair<string, string>[] parameters)
        {
            string resourceUri = uri.ToString();
            var filteredParameters = parameters.Where(p => p.Value != null);

            string paramsUri = string.Join("&",
                filteredParameters.Select(p => Uri.EscapeDataString(p.Key) + "=" + Uri.EscapeDataString(p.Value)));

            if (!string.IsNullOrEmpty(paramsUri))
            {
                resourceUri += "?" + paramsUri;
            }
            return new Uri(resourceUri, UriKind.Relative);
        }
    }
}