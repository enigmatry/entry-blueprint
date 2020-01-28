using System.Threading.Tasks;
using Refit;

namespace Enigmatry.Blueprint.Api.Features.GitHubApi
{
    public interface IGitHubClient
    {
        [Get("/users/{user}")]
        Task<User> GetUser(string user);
    }
}