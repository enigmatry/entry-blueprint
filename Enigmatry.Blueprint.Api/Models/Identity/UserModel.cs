using System;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Models.Identity
{
    [PublicAPI]
    public class UserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public GitHubApi.User? GitHubUser { get; set; }
    }
}
