using System;

namespace Enigmatry.Blueprint.Model
{
    public interface IEntityHasCreatedUpdated
    {
        void SetCreated(DateTime createdOn, int createdBy);
        void SetUpdated(DateTime updatedOn, int updatedBy);
    }
}