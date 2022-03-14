namespace Enigmatry.Blueprint.Core;

public static class EntityHasCreatedUpdatedExtensions
{
    public static T CreatedOn<T>(this T entity, DateTimeOffset createdOn, Guid createdBy)
        where T : IEntityHasCreatedUpdated
    {
        entity.SetCreated(createdOn, createdBy);
        return entity;
    }

    public static T UpdatedOn<T>(this T entity, DateTimeOffset updatedOn, Guid updatedBy)
        where T : IEntityHasCreatedUpdated
    {
        entity.SetUpdated(updatedOn, updatedBy);
        return entity;
    }
}
