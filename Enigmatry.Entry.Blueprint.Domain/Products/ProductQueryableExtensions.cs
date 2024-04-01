
namespace Enigmatry.Entry.Blueprint.Domain.Products;

public static class ProductQueryableExtensions
{
    public static IQueryable<Product> QueryByName(this IQueryable<Product> query, string? name) =>
        !String.IsNullOrEmpty(name)
            ? query.Where(e => e.Name.Contains(name))
            : query;

    public static IQueryable<Product> QueryByCode(this IQueryable<Product> query, string? code) =>
        !String.IsNullOrEmpty(code)
            ? query.Where(e => e.Code.Contains(code))
            : query;

    public static IQueryable<Product> QueryExpiresBefore(this IQueryable<Product> query, DateOnly? expiresBefore) =>
        expiresBefore is not null
            ? query.Where(e => e.ExpiresOn <= expiresBefore)
            : query;
    
    public static IQueryable<Product> QueryInStatus(this IQueryable<Product> query, ProductStatus status) =>
        query.Where(e => e.Status == status);
    
    public static IQueryable<Product> QueryCreatedBefore(this IQueryable<Product> query, DateTimeOffset dateTime) =>
        query.Where(e => e.CreatedOn < dateTime);
}
