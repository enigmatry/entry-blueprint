using Enigmatry.Entry.Blueprint.Domain.Auditing;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Entry.Blueprint.Domain.Products.DomainEvents;

public record ProductCreatedDomainEvent : AuditableDomainEvent
{
    public ProductCreatedDomainEvent(Product product, ILogger<ProductCreatedDomainEvent> logger) : base("ProductCreated")
    {
        logger.LogInformation("Handling Product Created Event.");
        Name = product.Name;
        Code = product.Code;
        Thread.Sleep(5000);
        logger.LogInformation("Handled Product Created Event.");
    }

    public string Name { get; }
    public string Code { get; }

    public override object AuditPayload => new { Name, Code };
}
