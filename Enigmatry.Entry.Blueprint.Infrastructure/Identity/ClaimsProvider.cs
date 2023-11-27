using System.Security.Claims;
using System.Security.Principal;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Identity;

public class ClaimsProvider : IClaimsProvider
{
    private static readonly string[] EmailClaims =
    {
        ClaimTypes.Upn,         // used in AzureAD 1.0 tokens
        "preferred_username",   // used in AzureAD 2.0 tokens
        "emails",               // used in AzureAD B2C tokens
        ClaimTypes.Email
    };

    private readonly Func<IPrincipal> _principalProvider;

    public ClaimsProvider(Func<IPrincipal> principalProvider)
    {
        _principalProvider = principalProvider;
    }

    private ClaimsPrincipal? Principal => _principalProvider() as ClaimsPrincipal;
    public bool IsAuthenticated => Principal?.Identity?.IsAuthenticated ?? false;

    public string? Email
    {
        get
        {
            if (Principal?.Identity is not ClaimsIdentity identity)
            {
                return null;
            }

            return identity.Claims.FirstOrDefault(claim => EmailClaims.Contains(claim.Type))?.Value;
        }
    }
}
