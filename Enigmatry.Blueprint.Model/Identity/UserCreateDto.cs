using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Model.Identity
{
    [PublicAPI]
    public class UserCreateDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
    }

    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
    }
}