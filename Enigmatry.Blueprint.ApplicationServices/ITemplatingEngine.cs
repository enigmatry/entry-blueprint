using System.Threading.Tasks;

namespace Enigmatry.Blueprint.ApplicationServices
{
   public interface ITemplatingEngine
    {
        Task<string> RenderFromFileAsync<T>(string path, T model);
    }
}
