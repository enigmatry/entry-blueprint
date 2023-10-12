
namespace Enigmatry.Blueprint.Domain.Products
{
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

        public static IQueryable<Product> QueryByContactEmail(this IQueryable<Product> query, string? email) =>
            !String.IsNullOrEmpty(email)
                ? query.Where(e => e.ContactEmail.Contains(email))
                : query;
    }
}
