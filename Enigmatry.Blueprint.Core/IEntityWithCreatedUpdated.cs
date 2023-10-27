namespace Enigmatry.Blueprint.Core;

public interface IEntityWithCreatedUpdated
{
    void SetCreated(DateTimeOffset createdOn, Guid createdBy);
    void SetUpdated(DateTimeOffset updatedOn, Guid updatedBy);
}
