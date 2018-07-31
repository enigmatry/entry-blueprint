using System;
using Enigmatry.Blueprint.Core.Settings;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules
{
    public static class ConfigurationExtensions
    {
        public static AppSettings ReadAppSettings(this IConfiguration configuration)
        {
            var appSettings = configuration.GetSection("App").Get<AppSettings>();
            if (appSettings == null)
            {
                throw new InvalidOperationException("App section is missing from configuration.");
            }
            return appSettings;
        }
    }
}