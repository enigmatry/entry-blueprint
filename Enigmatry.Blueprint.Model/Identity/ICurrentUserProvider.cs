namespace Enigmatry.Blueprint.Model.Identity
{
    public interface ICurrentUserProvider
    {
        int UserId { get; }
        User User { get; }
        bool IsAuthenticated { get; }
    }
}