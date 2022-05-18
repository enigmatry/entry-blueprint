using Enigmatry.Blueprint.Domain.Auditing;

namespace Enigmatry.Blueprint.Domain.Products.DomainEvents;

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
