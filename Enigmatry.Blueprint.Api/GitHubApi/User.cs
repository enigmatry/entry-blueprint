using Newtonsoft.Json;

namespace Enigmatry.Blueprint.Api.GitHubApi
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }

        [JsonProperty(PropertyName = "avatar_url")]
        public string AvatarUrl { get; set; }
    }
}