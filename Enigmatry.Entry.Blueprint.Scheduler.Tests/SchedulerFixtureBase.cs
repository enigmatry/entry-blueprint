using Autofac;
using Enigmatry.Entry.AspNetCore.Tests.Utilities;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Autofac;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Configuration;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Database;
using Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;
using Enigmatry.Entry.Blueprint.Infrastructure.Data;
using Enigmatry.Entry.Blueprint.Infrastructure.Tests;
using Enigmatry.Entry.Blueprint.Tests.Infrastructure.Impersonation;
using Enigmatry.Entry.Scheduler;
using Microsoft.EntityFrameworkCore;
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

        SeedTestUsers();
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
                // in the schedule jobs we do not need to replace current user, everything is executed under system user
                container.RegisterModule(new TestModule(false));

                container.RegisterAssemblyTypes(typeof(Program).Assembly)
                    .Where(t => t.Name.EndsWith("Job", StringComparison.InvariantCulture)).AsSelf();

                container.RegisterModule(new EntityFrameworkModule { RegisterMigrationsAssembly = true });
            });
    
    private void SeedTestUsers()
    {
        var dbContext = Resolve<DbContext>();
        new TestUserDataSeeding(dbContext).Seed();
    }

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
    
    protected void AddAndSaveChanges(params object[] entities)
    {
        var dbContext = Resolve<DbContext>();
        dbContext.AddRange(entities);
        dbContext.SaveChanges();
    }

    protected void AddToDbContext(params object[] entities)
    {
        var dbContext = Resolve<DbContext>();
        dbContext.AddRange(entities);
    }

    protected Task SaveChangesAsync() => Resolve<DbContext>().SaveChangesAsync();

    protected IQueryable<T> QueryDb<T>() where T : class => Resolve<DbContext>().Set<T>();

    protected IQueryable<T> QueryDbSkipCache<T>() where T : class => Resolve<DbContext>().QueryDbSkipCache<T>();

    protected Task DeleteByIdsAndSaveChangesAsync<T, TId>(params TId[] ids) where T : class =>
        Resolve<DbContext>().DeleteByIdsAndSaveChangesAsync<T, TId>(ids);

    private Task DeleteByIdAsync<T, TId>(TId id) where T : class => Resolve<DbContext>().DeleteByIdAsync<T, TId>(id);
    
    protected T Resolve<T>() where T : notnull => _testScope.Resolve<T>();
    
    protected void SetFixedUtcNow(DateTimeOffset value)
    {
        var settableTimeProvider = Resolve<SettableTimeProvider>();
        settableTimeProvider.SetNow(value);
    }
}
