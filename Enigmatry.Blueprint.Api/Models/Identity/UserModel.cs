using System;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Models.Identity
{
    [PublicAPI]
    public class UserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public GitHubApi.User GitHubUser { get; set; }
    }
}