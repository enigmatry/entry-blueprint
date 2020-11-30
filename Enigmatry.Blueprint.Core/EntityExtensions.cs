using System;

namespace Enigmatry.Blueprint.Core
{
    public static class EntityExtensions
    {
        public static T WithId<T>(this T entity, Guid id) where T : Entity
        {
            entity.Id = id;
            return entity;
        }
    }
}
