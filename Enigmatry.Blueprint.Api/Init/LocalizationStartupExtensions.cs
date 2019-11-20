using Enigmatry.Blueprint.Api.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Blueprint.Api.Init
{
    public static class LocalizationStartupExtensions
    {
        public static void AppAddLocalization(this IServiceCollection services)
        {
            // https://joonasw.net/view/aspnet-core-localization-deep-dive
            services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
        }

        public static IMvcBuilder AppAddLocalization(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(typeof(SharedResource));
            });
        }
    }
}
