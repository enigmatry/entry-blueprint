using System;

namespace Enigmatry.Blueprint.Model.Identity
{
    public class UserCreateDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}