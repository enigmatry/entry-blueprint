using Ardalis.SmartEnum;
using Enigmatry.Entry.SmartEnums.Entities;
using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Data.Configurations;

[UsedImplicitly]
public abstract class EntityWithEnumIdConfiguration<TEntity, TId>()
    : SmartEnums.EntityFramework.EntityWithEnumIdConfiguration<TEntity, TId>(NameMaxLength)
    where TEntity : EntityWithEnumId<TId>
    where TId : SmartEnum<TId>
{
    private const int NameMaxLength = 200;
}
