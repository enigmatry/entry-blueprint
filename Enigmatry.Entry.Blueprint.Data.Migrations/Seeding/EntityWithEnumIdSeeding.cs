using Ardalis.SmartEnum;
using Enigmatry.Entry.Blueprint.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Data.Migrations.Seeding;

internal class EntityWithEnumIdSeeding<T, TId> : ISeeding
    where T : EntityWithEnumId<TId>
    where TId : SmartEnum<TId>
{
    private readonly Func<TId, object> _entityFactory = id => new { Id = id, id.Name };

    public EntityWithEnumIdSeeding()
    {
    }

    public EntityWithEnumIdSeeding(Func<TId, object> entityFactory)
    {
        _entityFactory = entityFactory;
    }

    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (var id in SmartEnum<TId>.List)
        {
            modelBuilder.Entity<T>().HasData(_entityFactory(id));
        }
    }
}
