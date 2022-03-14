namespace Enigmatry.Blueprint.Core;

public interface IEntityHasCreatedUpdated
{
    void SetCreated(DateTimeOffset createdOn, Guid createdBy);
    void SetUpdated(DateTimeOffset updatedOn, Guid updatedBy);
}
