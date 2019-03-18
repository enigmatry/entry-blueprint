using System.Diagnostics;

using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Infrastructure.Templating;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.PlatformAbstractions;
using NUnit.Framework;

namespace Enigmatry.Blueprint.Infrastructure.Tests.Templating
{
    // Sample test that shows how to initialize RazorViewEngine and MVC in order to enable email templating capabilities from
    // non web types of application (e.g. console apps)
    // https://devblogs.microsoft.com/aspnet/testing-asp-net-core-mvc-web-apps-in-memory/
    // https://github.com/aspnet/Razor/issues/1212
    // https://codeopinion.com/using-razor-in-a-console-application/
    [Category("unit")]
    public class RazorTemplatingEngineFixture
    {
        private IServiceScopeFactory _scopeFactory;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            ServiceProvider serviceProvider = ConfigureDefaultServices(services);
            _scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        }

        [Test]
        public async Task TestRenderFromFile()
        {
            using (IServiceScope serviceScope = _scopeFactory.CreateScope())
            {
                var engine = serviceScope.ServiceProvider.GetRequiredService<RazorTemplatingEngine>();

                string result = await engine.RenderFromFileAsync("~/Templating/Sample.cshtml", new EmailModel
                    {SampleText = "Hello world!"});
               
                result.Should().Contain("Hello world!");
                result.Should().Contain("Congratulations!");
            }
        }

        private ServiceProvider ConfigureDefaultServices(ServiceCollection services)
        {
            ApplicationEnvironment applicationEnvironment = PlatformServices.Default.Application;
            services.AddSingleton(applicationEnvironment);

            string appDirectory = Directory.GetCurrentDirectory();

            var environment = new HostingEnvironment
            {
                ApplicationName = Assembly.GetEntryAssembly().GetName().Name
            };
            services.AddSingleton<IHostingEnvironment>(environment);

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Clear();
                options.FileProviders.Add(new PhysicalFileProvider(appDirectory));
            });

            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

            var diagnosticSource = new DiagnosticListener("Microsoft.AspNetCore");
            services.AddSingleton<DiagnosticSource>(diagnosticSource);

            services.AddLogging();
            services.AddMvc();
            services.AddSingleton<RazorTemplatingEngine>();
            ServiceProvider provider = services.BuildServiceProvider();
            return provider;
        }
    }
}
