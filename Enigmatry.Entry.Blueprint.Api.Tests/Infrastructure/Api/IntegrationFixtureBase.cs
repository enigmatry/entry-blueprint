using System.Reflection;
using Enigmatry.Entry.AspNetCore.Tests.SystemTextJson.Http;
using Enigmatry.Entry.AspNetCore.Tests.Utilities;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Configuration;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Database;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.Blueprint.Infrastructure.Data;
using Enigmatry.Entry.Blueprint.Infrastructure.Tests;
using Enigmatry.Entry.Blueprint.Tests.Infrastructure.Impersonation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Api;

public class IntegrationFixtureBase
{
    private IConfiguration _configuration = null!;
    private TestDatabase _testDatabase = null!;
    private IServiceScope _testScope = null!;
    private static ApiWebApplicationFactory _factory = null!;
    private bool _isUserAuthenticated = true;
    protected HttpClient Client { get; private set; } = null!;

    [SetUp]
    protected async Task Setup()
    {
        _testDatabase = new TestDatabase();

        _configuration = new TestConfigurationBuilder()
            .WithDbContextName(nameof(AppDbContext))
            .WithConnectionString(_testDatabase.ConnectionString)
            .Build();

        _factory = new ApiWebApplicationFactory(_configuration, _isUserAuthenticated);

        var scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _testScope = scopeFactory.CreateScope();
        Client = _factory.CreateClient();

        var dbContext = Resolve<DbContext>();
        await TestDatabase.ResetAsync(dbContext);

        SeedTestUsers();

        AddApiJsonConverters();
    }

    protected void DisableUserAuthentication() => _isUserAuthenticated = false;

    private void SeedTestUsers()
    {
        var dbContext = Resolve<DbContext>();
        new TestUserDataSeeding(dbContext).Seed();
    }

    [TearDown]
    public void Teardown()
    {
        _factory.Dispose();
        _testScope.Dispose();
        Client.Dispose();
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

    private static void AddApiJsonConverters()
    {
        var converters = HttpSerializationOptions.Options.Converters;

        // Guard if multiple tests are run in one context.
        if (converters.Count > 0)
        {
            return;
        }
        converters.AppRegisterJsonConverters();
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
