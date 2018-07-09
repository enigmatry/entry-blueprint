namespace Enigmatry.Blueprint.Model.Identity
{
    public interface ICurrentUserProvider
    {
        User User { get; }
        bool IsAuthenticated { get; }
    }
}