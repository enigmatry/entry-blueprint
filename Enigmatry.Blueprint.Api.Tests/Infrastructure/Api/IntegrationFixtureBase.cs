using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using Autofac.Extensions.DependencyInjection;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Configuration;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Database;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;
using Enigmatry.Blueprint.Model.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.Api
{
    public class IntegrationFixtureBase
    {
#pragma warning disable CS8618 // Initialized in Setup
        private IConfiguration _configuration;
        private TestServer _server;
        private IServiceScope _testScope;
        protected HttpClient Client;
        protected bool _seedUsers = true;
#pragma warning restore CS8618 //

        [SetUp]
        protected void Setup()
        {
            _configuration = new TestConfigurationBuilder()
                .WithDbContextName("BlueprintContext")
                .Build();

            IWebHostBuilder webHostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddAutofac())
                .UseConfiguration(_configuration)
                .UseStartup<TestStartup>();

            _server = new TestServer(webHostBuilder);
            CreateDatabase();
            
            Client = _server.CreateClient();
            _testScope = CreateScope();
            SeedUsers();
        }

        private IServiceScope CreateScope() => _server.Host.Services.CreateScope();

        private void CreateDatabase()
        {
            using IServiceScope scope = CreateScope();
            var dbContext = scope.Resolve<BlueprintContext>();
            // On Azure we cannot drop db, we can only delete all tables
            DropAllDbObjects(dbContext.Database);
            // In case that we want to delete db call: dbContext.Database.EnsureDeleted()
            dbContext.Database.Migrate();
        }

        private static void DropAllDbObjects(DatabaseFacade database)
        {
            try
            {
                string dropAllSql = EmbeddedResource.ReadResourceContent("Enigmatry.Blueprint.Api.Tests.Infrastructure.Database.DropAllSql.sql");
                foreach (var statement in dropAllSql.SplitStatements())
                    // WriteLine("Executing: " + statement);
                    database.ExecuteSqlRaw(statement);
            }
            catch (SqlException ex)
            {
                const int cannotOpenDatabaseErrorNumber = 4060;
                if (ex.Number == cannotOpenDatabaseErrorNumber)
                {
                    WriteLine("Error while trying to drop all objects from database. Maybe database does not exist.");
                    WriteLine("Continuing...");
                    WriteLine(ex.ToString());
                }
                else
                {
                    throw;
                }
            }
        }

        protected void DoNotSeedUsers()
        {
            _seedUsers = false;
        }

        private void SeedUsers()
        {
            if (_seedUsers)
            {
                AddCurrentUserToDb();
            }
        }

        private void AddCurrentUserToDb()
        {
            using IServiceScope scope = CreateScope();
            var currentUserProvider = scope.Resolve<ICurrentUserProvider>();
            var dbContext = scope.Resolve<DbContext>();

            dbContext.Add(currentUserProvider.User);
            dbContext.SaveChanges();
        }

        [TearDown]
        public void Teardown()
        {
            _testScope.Dispose();
            Client.Dispose();
            _server.Dispose();
        }

        protected void AddAndSaveChanges<T>(params T[] entities)
        {
            var dbContext = Resolve<DbContext>();

            foreach (T entity in entities)
            {
                dbContext.Add(entity);
            }

            dbContext.SaveChanges();
        }

        protected void AddAndSaveChanges(params object[] entities)
        {
            var dbContext = Resolve<DbContext>();

            foreach (object entity in entities)
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

            foreach (object entity in entities)
            {
                dbContext.Add(entity);
            }
        }

        protected void SaveChanges()
        {
            var unitOfWork = _testScope.Resolve<IUnitOfWork>();
            unitOfWork.SaveChanges();
        }

        protected IQueryable<T> QueryDb<T>() where T : class =>
            Resolve<DbContext>().Set<T>();

        protected IQueryable<T> QueryDbSkipCache<T>() where T : class =>
            Resolve<DbContext>().Set<T>().AsNoTracking();

        protected T Resolve<T>() => _testScope.Resolve<T>();

        private static void WriteLine(string message) => TestContext.WriteLine(message);
    }
}
