using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Infrastructure.Data.Configurations;
using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework.MediatR;
using Enigmatry.Blueprint.Model;
using Enigmatry.Blueprint.Model.Identity;
using Enigmatry.BuildingBlocks.IntegrationEventLogEF;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Infrastructure.Data.EntityFramework
{
    [UsedImplicitly]
    public class BlueprintContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly ITimeProvider _timeProvider;

        public Action<ModelBuilder> ModelBuilderConfigurator { private get; set; }

        public BlueprintContext(DbContextOptions options, IMediator mediator, ITimeProvider timeProvider) : base(options)
        {
            _mediator = mediator;
            _timeProvider = timeProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEntityTypeConfiguration(typeof(UserConfiguration).Assembly);
            modelBuilder.UseEntityTypeConfiguration(typeof(IntegrationEventLogEntryConfiguration).Assembly);

            RegisterEntities(modelBuilder);

            ModelBuilderConfigurator?.Invoke(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void RegisterEntities(ModelBuilder modelBuilder)
        {
            MethodInfo entityMethod = typeof(ModelBuilder).GetMethods().First(m => m.Name == "Entity" && m.IsGenericMethod);

            IEnumerable<Type> entityTypes = Assembly.GetAssembly(typeof(User)).GetTypes()
                .Where(x => x.IsSubclassOf(typeof(Entity)) && !x.IsAbstract);

            foreach (Type type in entityTypes)
            {
                entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
            }
        }

        public async Task<int> SaveEntitiesAsync(Guid currentUserId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            PopulateCreatedUpdated(currentUserId);
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // TODO: call populate created updated again?

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            return await SaveChangesAsync(cancellationToken);
        }

        private void PopulateCreatedUpdated(Guid currentUserId)
        {
            var changedEntities = ChangeTracker
                .Entries<Entity>()
                .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified) &&
                            x.Entity is IEntityHasCreatedUpdated)
                .Select(x => new {x.State, Entity = (IEntityHasCreatedUpdated) x.Entity}).ToList();

            foreach (var entity in changedEntities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.SetCreated(_timeProvider.Now, currentUserId);
                    entity.Entity.SetUpdated(_timeProvider.Now, currentUserId);
                }

                if (entity.State == EntityState.Modified)
                    entity.Entity.SetUpdated(_timeProvider.Now, currentUserId);
            }
        }
    }
}