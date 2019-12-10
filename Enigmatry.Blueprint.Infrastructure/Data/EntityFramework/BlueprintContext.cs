using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Core.Helpers;
using Enigmatry.Blueprint.Infrastructure.Data.Configurations;
using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework.MediatR;
using Enigmatry.Blueprint.Model;
using Enigmatry.Blueprint.Model.Identity;
using Enigmatry.BuildingBlocks.IntegrationEventLogEF;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Infrastructure.Data.EntityFramework
{
    [UsedImplicitly]
    public class BlueprintContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly ITimeProvider _timeProvider;
        private readonly ICurrentUserIdProvider _currentUserIdProvider;
        private readonly ILogger<BlueprintContext> _logger;
        private readonly IDbContextAccessTokenProvider _dbContextAccessTokenProvider;
        public Action<ModelBuilder>? ModelBuilderConfigurator { private get; set; }

        public BlueprintContext(DbContextOptions options, IMediator mediator, ITimeProvider timeProvider, ICurrentUserIdProvider currentUserIdProvider, ILogger<BlueprintContext> logger, IDbContextAccessTokenProvider dbContextAccessTokenProvider) : base(options)
        {
            _mediator = mediator;
            _timeProvider = timeProvider;
            _currentUserIdProvider = currentUserIdProvider;
            _logger = logger;
            _dbContextAccessTokenProvider = dbContextAccessTokenProvider;

            SetupManagedServiceIdentityAccessToken();
        }

        private void SetupManagedServiceIdentityAccessToken()
        {
            var accessToken = _dbContextAccessTokenProvider.GetAccessTokenAsync().GetAwaiter().GetResult();
            if (accessToken.HasContent())
            {
                var connection = (SqlConnection)Database.GetDbConnection();
                connection.AccessToken = accessToken;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEntityTypeConfiguration(typeof(UserConfiguration).Assembly);
            modelBuilder.UseEntityTypeConfiguration(typeof(IntegrationEventLogEntryConfiguration).Assembly);

            RegisterEntities(modelBuilder);

            ModelBuilderConfigurator?.Invoke(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void RegisterEntities(ModelBuilder modelBuilder)
        {
            MethodInfo entityMethod = typeof(ModelBuilder).GetMethods().First(m => m.Name == "Entity" && m.IsGenericMethod);

            Assembly? entitiesAssembly = Assembly.GetAssembly(typeof(User));
            var types = entitiesAssembly != null? entitiesAssembly.GetTypes(): Enumerable.Empty<Type>();

            IEnumerable<Type> entityTypes = types
                    .Where(x => x.IsSubclassOf(typeof(Entity)) && !x.IsAbstract);

            foreach (Type type in entityTypes)
            {
                entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
            }
        }

        public override int SaveChanges()
        {
            var task = Task.Run(async () => await SaveChangesAsync());
            return task.GetAwaiter().GetResult();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            PopulateCreatedUpdated();

            // we need to gather domain events before saving, so that we include events
            // for deleted entities (otherwise they are lost due to deletion of the object from context)
            IEnumerable<INotification> domainEvents = this.GatherDomainEventsFromContext();

            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var saved = await base.SaveChangesAsync(cancellationToken);

            await _mediator.DispatchDomainEventsAsync(domainEvents, _logger);

            return saved;
        }

        private void PopulateCreatedUpdated()
        {
            Guid? userId = _currentUserIdProvider.IsAuthenticated
                ? _currentUserIdProvider.UserId
                : null;

            var changedEntities = ChangeTracker
                .Entries<Entity>()
                .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified) &&
                            x.Entity is IEntityHasCreatedUpdated)
                .Select(x => new {x.State, Entity = (IEntityHasCreatedUpdated)x.Entity}).ToList();

            if (userId.HasValue)
            {
                foreach (var entity in changedEntities)
                {
                    if (entity.State == EntityState.Added)
                    {
                        entity.Entity.SetCreated(_timeProvider.Now, userId.Value);
                        entity.Entity.SetUpdated(_timeProvider.Now, userId.Value);
                    }

                    if (entity.State == EntityState.Modified)
                        entity.Entity.SetUpdated(_timeProvider.Now, userId.Value);
                }
            }
        }
    }
}
