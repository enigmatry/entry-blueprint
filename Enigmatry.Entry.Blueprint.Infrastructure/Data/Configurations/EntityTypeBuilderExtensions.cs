using System.Linq.Expressions;
using Ardalis.SmartEnum;
using Enigmatry.Entry.Blueprint.Core.Entities;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.SmartEnums.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Data.Configurations;

public static class EntityTypeBuilderExtensions
{
    public static void HasEnumId<T, TId>(this EntityTypeBuilder<T> builder)
        where T : EntityWithEnumId<TId> where TId : SmartEnum<TId>
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasSmartEnumConversion()
            .ValueGeneratedNever();
    }

    public static void HasLookupTableRelation<TEntity, TLookupEntity, TId>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, TLookupEntity?>> referenceSelector,
        Expression<Func<TEntity, TId>> keySelector)
        where TEntity : class
        where TLookupEntity : class
        where TId : SmartEnum<TId>
    {
        builder.Property(keySelector).HasSmartEnumConversion();
        builder.HasOne(referenceSelector)
            .WithMany()
            .HasForeignKey(((MemberExpression)keySelector.Body).Member.Name)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public static void HasNullableLookupTableRelation<TEntity, TLookup, TId>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, TLookup?>> referenceSelector,
        Expression<Func<TEntity, TId?>> keySelector)
        where TEntity : class
        where TLookup : class
        where TId : SmartEnum<TId>
    {
        builder.Property(keySelector).HasNullableSmartEnumConversion();
        builder.HasOne(referenceSelector)
            .WithMany()
            .HasForeignKey(((MemberExpression)keySelector.Body).Member.Name)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public static void HasCreatedByAndUpdatedBy<T>(this EntityTypeBuilder<T> builder) where T : EntityWithCreatedUpdated
    {
        builder.HasOne<User>().WithMany().HasForeignKey(e => e.CreatedById).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne<User>().WithMany().HasForeignKey(e => e.UpdatedById).OnDelete(DeleteBehavior.NoAction);
    }
}
