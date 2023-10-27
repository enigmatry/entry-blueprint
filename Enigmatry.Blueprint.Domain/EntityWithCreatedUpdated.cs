using Enigmatry.Blueprint.Core;
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Blueprint.Domain;

public abstract class EntityWithCreatedUpdated : EntityWithGuidId, IEntityWithCreatedUpdated
{
    public Guid? CreatedById { get; private set; }
    public Guid? UpdatedById { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; }
    public DateTimeOffset UpdatedOn { get; private set; }

    public void SetCreated(DateTimeOffset createdOn, Guid createdBy)
    {
        CreatedOn = createdOn;
        CreatedById = createdBy;
    }

    public void SetUpdated(DateTimeOffset updatedOn, Guid updatedBy)
    {
        UpdatedOn = updatedOn;
        UpdatedById = updatedBy;
    }
}
