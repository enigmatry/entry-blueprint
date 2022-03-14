using Enigmatry.Blueprint.Model.Auditing;

namespace Enigmatry.Blueprint.Model.Products.DomainEvents;

public record ProductUpdatedDomainEvent : AuditableDomainEvent
{
    public ProductUpdatedDomainEvent(Product product) : base("ProductUpdated")
    {
        Name = product.Name;
        Code = product.Code;
    }

    public string Name { get; }
    public string Code { get; }

    public override object AuditPayload => new { Name, Code };
}
