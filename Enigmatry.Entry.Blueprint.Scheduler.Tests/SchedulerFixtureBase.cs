using Autofac;
using Enigmatry.Entry.AspNetCore.Tests.Utilities;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Autofac;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Configuration;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Database;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Impersonation;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;
using Enigmatry.Entry.Blueprint.Infrastructure.Data;
using Enigmatry.Entry.Scheduler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Enigmatry.Entry.Blueprint.Scheduler.Tests;

[Category("integration")]
public abstract class SchedulerFixtureBase
{
    private IHost _host = null!;
    private IServiceScope _testScope = null!;

    [SetUp]
    public async Task BaseSetup()
    {
        var database = new TestDatabase();

        var configuration = new TestConfigurationBuilder()
            .WithDbContextName(nameof(AppDbContext))
            .WithConnectionString(database.ConnectionString)
            .BuildSchedulerConfiguration();

        _host = CreateHostBuilder(configuration).Build();

        _testScope = _host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        await TestDatabase.ResetAsync(_testScope.Resolve<AppDbContext>());

        SeedTestUser();
    }

    private static IHostBuilder CreateHostBuilder(IConfiguration configuration) =>
        Program.CreateHostBuilder()
            .ConfigureAppConfiguration((_, configurationBuilder) =>
            {
                configurationBuilder.Sources.Clear();
                configurationBuilder.AddConfiguration(configuration);
            })
            .ConfigureContainer<ContainerBuilder>(container =>
            {
                container.RegisterModule<TestModule>();

                container.RegisterAssemblyTypes(typeof(Program).Assembly)
                    .Where(t => t.Name.EndsWith("Job", StringComparison.InvariantCulture)).AsSelf();

                container.RegisterModule(new EntityFrameworkModule { RegisterMigrationsAssembly = true });
            });

    private void SeedTestUser()
    {
        var dbContext = Resolve<AppDbContext>();
        var testUser = TestUserData.CreateSchedulerUser();
        if (dbContext.Set<User>().SingleOrDefault(x => x.Id == testUser.Id) != null)
        {
            return;
        }
        dbContext.Set<User>().Add(testUser);
        dbContext.SaveChanges();
    }

    protected T Resolve<T>() where T : notnull => _testScope.Resolve<T>();

    protected async Task ExecuteJob<TJob, TOptions>(TOptions options) where TJob : EntryJob<TOptions> where TOptions : class, new()
    {
        using var scope = _host.Services.CreateScope();
        var job = scope.Resolve<TJob>();
        await job.Execute(options);
    }

    [TearDown]
    public void TearDown()
    {
        _host?.Dispose();
        _testScope?.Dispose();
    }
}
