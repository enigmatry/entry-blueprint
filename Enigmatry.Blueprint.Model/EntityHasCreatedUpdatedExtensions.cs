using System;

namespace Enigmatry.Blueprint.Model
{
    public static class EntityHasCreatedUpdatedExtensions
    {
        public static T CreatedOn<T>(this T entity, DateTimeOffset createdOn, int createdBy)
            where T : IEntityHasCreatedUpdated
        {
            entity.SetCreated(createdOn, createdBy);
            return entity;
        }

        public static T UpdatedOn<T>(this T entity, DateTimeOffset updatedOn, int updatedBy)
            where T : IEntityHasCreatedUpdated
        {
            entity.SetUpdated(updatedOn, updatedBy);
            return entity;
        }
    }
}