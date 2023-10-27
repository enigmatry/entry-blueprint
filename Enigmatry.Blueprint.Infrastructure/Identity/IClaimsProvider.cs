namespace Enigmatry.Blueprint.Infrastructure.Identity;

public interface IClaimsProvider
{
    public bool IsAuthenticated { get; }
    public string? Email { get; }
}
