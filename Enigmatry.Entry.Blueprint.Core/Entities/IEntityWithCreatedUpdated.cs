namespace Enigmatry.Entry.Blueprint.Core.Entities;

public interface IEntityWithCreatedUpdated
{
    public void SetCreated(DateTimeOffset createdOn, Guid createdBy);

    public void SetUpdated(DateTimeOffset updatedOn, Guid updatedBy);
}
