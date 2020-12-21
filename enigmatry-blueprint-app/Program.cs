using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Enigmatry.Blueprint.App
{
    public static class Program
    {
        public static void Main(string[] args) => BuildWebHost(args).Run();

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureKestrel(options => options.AddServerHeader = false)
                .UseStartup<Startup>()
                .Build();
    }
}
