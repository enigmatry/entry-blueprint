using Enigmatry.Blueprint.Infrastructure.Templating;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Enigmatry.Blueprint.Infrastructure.Tests
{
    // example startup class - show how to initialize Razor in console application
    // and RazorTemplatingEngine class. This class can be used for email templating purposes.
    public class RazorSampleConsoleStartup
    {
        private readonly IConfiguration _configuration;

        public RazorSampleConsoleStartup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string appDirectory = _configuration.GetValue<string>("App:RazorViewsRootDirectory");
            services.AddRazorPages().AddRazorRuntimeCompilation(options =>
            {
                options.FileProviders.Clear();
                options.FileProviders.Add(new PhysicalFileProvider(appDirectory));
            });

            services.AddSingleton<RazorTemplatingEngine>();
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
