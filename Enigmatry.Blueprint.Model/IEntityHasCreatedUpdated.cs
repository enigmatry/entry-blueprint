using System;

namespace Enigmatry.Blueprint.Model
{
    public interface IEntityHasCreatedUpdated
    {
        void SetCreated(DateTimeOffset createdOn, int createdBy);
        void SetUpdated(DateTimeOffset updatedOn, int updatedBy);
    }
}