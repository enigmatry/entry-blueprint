
namespace Enigmatry.Entry.Blueprint.Domain.Products;

public static class ProductQueryableExtensions
{
    extension(IQueryable<Product> query)
    {
        public IQueryable<Product> QueryByName(string? name) =>
            name is null or "" ? query : query.Where(e => e.Name.Contains(name));

        public IQueryable<Product> QueryByCode(string? code) =>
            code is null or "" ? query : query.Where(e => e.Code.Contains(code));

        public IQueryable<Product> QueryExpiresBefore(DateOnly? expiresBefore) =>
            expiresBefore is not null
                ? query.Where(e => e.ExpiresOn <= expiresBefore)
                : query;

        public IQueryable<Product> QueryInStatus(ProductStatus status) =>
            query.Where(e => e.Status == status);

        public IQueryable<Product> QueryCreatedBefore(DateTimeOffset dateTime) =>
            query.Where(e => e.CreatedOn < dateTime);
    }
}
