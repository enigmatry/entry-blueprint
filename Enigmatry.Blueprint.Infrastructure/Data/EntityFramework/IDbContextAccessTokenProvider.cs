using System.Threading.Tasks;

namespace Enigmatry.Blueprint.Infrastructure.Data.EntityFramework
{
    public interface IDbContextAccessTokenProvider
    {
        Task<string> GetAccessTokenAsync();
    }
}
