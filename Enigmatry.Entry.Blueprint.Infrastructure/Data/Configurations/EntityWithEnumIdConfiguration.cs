using Ardalis.SmartEnum;
using Enigmatry.Entry.Blueprint.Core.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Data.Configurations;

[UsedImplicitly]
public abstract class EntityWithEnumIdConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityWithEnumId<TId> where TId : SmartEnum<TId>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasEnumId<TEntity, TId>();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
    }
}
