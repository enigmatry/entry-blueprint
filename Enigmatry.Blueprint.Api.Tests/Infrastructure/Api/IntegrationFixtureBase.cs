using Autofac.Extensions.DependencyInjection;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Configuration;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;
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
        protected JsonHttpClient Client;

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
            Client = new JsonHttpClient(_server.CreateClient());
            _testScope = CreateScope();
        }

        private IServiceScope CreateScope()
        {
            return _server.Host.Services.CreateScope();
        }

        private void CreateDatabase()
        {
            using (IServiceScope scope = CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<BlueprintContext>();
                dbContext.Database.EnsureDeleted();
                dbContext.Database.Migrate();
            }
        }

        [TearDown]
        public void Teardown()
        {
            _testScope.Dispose();
            Client.Dispose();
            _server.Dispose();
        }

        protected void SaveChanges()
        {
            var unitOfWork = Resolve<IUnitOfWork>();
            unitOfWork.SaveChanges();
        }

        protected void AddToRepository<T>(T entity) where T : Entity
        {
            var repository = Resolve<IRepository<T>>();
            repository.Add(entity);
        }

        protected T Resolve<T>()
        {
            return _testScope.ServiceProvider.GetRequiredService<T>();
        }
    }
}