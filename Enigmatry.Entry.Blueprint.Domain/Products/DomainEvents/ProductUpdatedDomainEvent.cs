using Enigmatry.Entry.Blueprint.Domain.Auditing;

namespace Enigmatry.Entry.Blueprint.Domain.Products.DomainEvents;

public record ProductUpdatedDomainEvent : AuditableDomainEvent
{
    public ProductUpdatedDomainEvent(Product product) : base("ProductUpdated")
    {
        Name = product.Name;
        Code = product.Code;
        Status = product.Status;
    }

    public ProductStatus Status { get; set; }
    public string Name { get; }
    public string Code { get; }

    public override object AuditPayload => new { Name, Code, Status };
}
