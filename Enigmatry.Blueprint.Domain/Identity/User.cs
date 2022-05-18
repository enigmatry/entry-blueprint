using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Domain.Identity.Commands;
using Enigmatry.Blueprint.Domain.Identity.DomainEvents;
using Enigmatry.Blueprint.Domain.Products;
using Enigmatry.BuildingBlocks.Core.Entities;

namespace Enigmatry.Blueprint.Domain.Identity;

public class User : EntityWithGuidId, IEntityHasCreatedUpdated
{
    public static readonly Guid TestUserId = new("8207DB25-94D1-4F3D-BF18-90DA283221F7");
    public const int NameMinLenght = 5;
    public const int NameMaxLenght = 25;

    public string UserName { get; private set; } = "";
    public string Name { get; private set; } = "";
    public DateTimeOffset CreatedOn { get; private set; }
    public DateTimeOffset UpdatedOn { get; private set; }
    public Guid? CreatedById { get; private set; }
    public Guid? UpdatedById { get; private set; }

    public User? CreatedBy { get; private set; }
    public User? UpdatedBy { get; private set; }

    public ICollection<User>? CreatedUsers { get; private set; }
    public ICollection<User>? UpdatedUsers { get; private set; }
    public ICollection<Product>? CreatedProducts { get; private set; }
    public ICollection<Product>? UpdatedProducts { get; private set; }

    public static User Create(CreateOrUpdateUser.Command command)
    {
        var result = new User
        {
            UserName = command.UserName,
            Name = command.Name,
        };

        result.AddDomainEvent(new UserCreatedDomainEvent(result.UserName));
        return result;
    }

    public void Update(CreateOrUpdateUser.Command command)
    {
        Name = command.Name;
        AddDomainEvent(new UserUpdatedDomainEvent(UserName));
    }

    public void SetCreated(DateTimeOffset createdOn, Guid createdBy)
    {
        SetCreated(createdOn);
        CreatedById = createdBy;
    }

    public void SetCreated(DateTimeOffset createdOn) => CreatedOn = createdOn;

    public void SetUpdated(DateTimeOffset updatedOn, Guid updatedBy)
    {
        SetUpdated(updatedOn);
        UpdatedById = updatedBy;
    }

    public void SetUpdated(DateTimeOffset updatedOn) => UpdatedOn = updatedOn;
}
