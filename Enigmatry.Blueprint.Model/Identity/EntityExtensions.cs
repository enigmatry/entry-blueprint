using Enigmatry.Blueprint.Core;

namespace Enigmatry.Blueprint.Model.Identity
{
    public static class EntityExtensions
    {
        public static T WithId<T>(this T entity, int id) where T: Entity
        {
            entity.Id = id;
            return entity;
        }
    }
}