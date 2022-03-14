var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
  options.AddServerHeader = false;
});

builder.Services.AddSpaStaticFiles(configuration => { configuration.RootPath = "dist"; });

var app = builder.Build();

app.UseSpaStaticFiles();
app.UseSpa(spa =>
{
  // To learn more about options for serving an Angular SPA from ASP.NET Core,
  // see https://go.microsoft.com/fwlink/?linkid=864501
  spa.Options.SourcePath = "dist";
});

app.Run();

