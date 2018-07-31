using System.Net.Http;
using Autofac.Extensions.DependencyInjection;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Configuration;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;
using Enigmatry.Blueprint.Model.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.Api
{
    public class IntegrationFixtureBase
    {
        private IConfiguration _configuration;
        private TestServer _server;
        private IServiceScope _testScope;
        protected JsonHttpClient JsonClient;
        protected HttpClient Client;

        [SetUp]
        protected void Setup()
        {
            _configuration = new TestConfigurationBuilder()
                .WithConnectionString(
                    "Server=.;Database=Blueprint-Core-integration-testing;Trusted_Connection=True;MultipleActiveResultSets=true")
                .WithDbContextName("BlueprintContext")
                .Build();

            IWebHostBuilder webHostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddAutofac())
                .UseConfiguration(_configuration)
                .UseStartup<TestStartup>();

            _server = new TestServer(webHostBuilder);
            CreateDatabase();
            
            Client = _server.CreateClient();
            JsonClient = new JsonHttpClient(Client);
            _testScope = CreateScope();
            AddCurrentUserToDb();
        }

        private IServiceScope CreateScope()
        {
            return _server.Host.Services.CreateScope();
        }

        private void CreateDatabase()
        {
            using (IServiceScope scope = CreateScope())
            {
                var dbContext = scope.Resolve<BlueprintContext>();
                dbContext.Database.EnsureDeleted();
                dbContext.Database.Migrate();
            }
        }

        private void AddCurrentUserToDb()
        {
            using (IServiceScope scope = CreateScope())
            {
                var currentUserProvider = scope.Resolve<ICurrentUserProvider>();
                var dbContext = scope.Resolve<DbContext>();

                dbContext.Add(currentUserProvider.User);
                dbContext.SaveChanges();
            }
        }

        [TearDown]
        public void Teardown()
        {
            _testScope.Dispose();
            JsonClient.Dispose();
            _server.Dispose();
        }

        protected void SaveChanges()
        {
            var unitOfWork = _testScope.Resolve<IUnitOfWork>();
            unitOfWork.SaveChanges();
        }

        protected T Resolve<T>()
        {
            return _testScope.Resolve<T>();
        }

    }
}