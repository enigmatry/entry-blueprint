namespace Enigmatry.Blueprint.Domain.Authorization
{
    public enum PermissionId
    {
        None = 0,

        UsersRead = 1,
        UsersWrite = 2,

        ProductsRead = 10,
        ProductsWrite = 11,
        ProductsDelete = 12
    }
}
