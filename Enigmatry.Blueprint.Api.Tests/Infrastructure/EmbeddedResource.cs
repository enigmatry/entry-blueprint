using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure
{
    public static class EmbeddedResource
    {
        public static string ReadResourceContent(string namespaceAndFileName)
        {
            try
            {
                using (Stream stream = typeof(EmbeddedResource).GetTypeInfo().Assembly
                    .GetManifestResourceStream(namespaceAndFileName))
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Failed to read Embedded Resource {namespaceAndFileName}", exception);
            }
        }
    }
}