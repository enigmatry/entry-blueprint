using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Configuration;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Database;
using Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Impersonation;
using Enigmatry.Entry.Blueprint.Infrastructure.Data;
using Enigmatry.Entry.AspNetCore.Tests.Utilities;
using Enigmatry.Entry.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Entry.Blueprint.Api.Tests.Infrastructure.Api;

public class IntegrationFixtureBase
{
    private IConfiguration _configuration = null!;
    private TestDatabase _testDatabase = null!;
    private IServiceScope _testScope = null!;
    private static BlueprintWebApplicationFactory _factory = null!;
    private bool _seedUsers = true;
    private bool _isUserAuthenticated = true;

    protected HttpClient Client { get; private set; } = null!;

    [SetUp]
    protected async Task Setup()
    {
        _testDatabase = new TestDatabase();

        _configuration = new TestConfigurationBuilder()
            .WithDbContextName(nameof(BlueprintContext))
            .WithConnectionString(_testDatabase.ConnectionString)
            .Build();

        _factory = new BlueprintWebApplicationFactory(_configuration, _isUserAuthenticated);

        var scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _testScope = scopeFactory.CreateScope();
        Client = _factory.CreateClient();

        var dbContext = Resolve<DbContext>();
        await TestDatabase.ResetAsync(dbContext);

        SeedUsers();
    }

    protected void DoNotSeedUsers() => _seedUsers = false;

    protected void DisableUserAuthentication() => _isUserAuthenticated = false;

    private void SeedUsers()
    {
        if (_seedUsers)
        {
            AddCurrentUserToDb();
        }
    }

    private void AddCurrentUserToDb()
    {
        var dbContext = Resolve<DbContext>();
        dbContext.Add(TestUserData.CreateTestUser());
        dbContext.SaveChanges();
    }

    [TearDown]
    public void Teardown()
    {
        _factory.Dispose();
        _testScope.Dispose();
        Client.Dispose();
    }

    protected void AddAndSaveChanges<T>(params T[] entities)
    {
        var dbContext = Resolve<DbContext>();

        foreach (var entity in entities)
        {
            dbContext.Add(entity!);
        }

        dbContext.SaveChanges();
    }

    protected void AddAndSaveChanges(params object[] entities)
    {
        var dbContext = Resolve<DbContext>();

        foreach (var entity in entities)
        {
            dbContext.Add(entity);
        }

        dbContext.SaveChanges();
    }

    protected void AddToContext(params object[] entities) =>
        AddToContext(entities.AsEnumerable());

    protected void AddToContext(IEnumerable<object> entities)
    {
        var dbContext = Resolve<DbContext>();

        foreach (var entity in entities)
        {
            dbContext.Add(entity);
        }
    }

    protected async Task SaveChanges()
    {
        var unitOfWork = Resolve<IUnitOfWork>();
        await unitOfWork.SaveChangesAsync();
    }

    protected IQueryable<T> QueryDb<T>() where T : class =>
        Resolve<DbContext>().Set<T>();

    protected IQueryable<T> QueryDbSkipCache<T>() where T : class =>
        Resolve<DbContext>().Set<T>().AsNoTracking();

    protected async Task DeleteByIdsAndSaveChanges<T, TId>(params TId[] ids) where T : class
    {
        foreach (var id in ids)
        {
            DeleteById<T, TId>(id);
        }

        await SaveChanges();
    }

    protected T Resolve<T>() where T : notnull => _testScope.Resolve<T>();

    private void DeleteById<T, TId>(TId id) where T : class
    {
        var dbSet = Resolve<DbContext>().Set<T>();
        var entity = dbSet.Find(id);
        if (entity != null)
        {
            dbSet.Remove(entity);
        }
    }

}
