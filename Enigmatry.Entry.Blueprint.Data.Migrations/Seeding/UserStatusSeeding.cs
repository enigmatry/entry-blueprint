using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.SmartEnums.EntityFramework.Seeding;

namespace Enigmatry.Entry.Blueprint.Data.Migrations.Seeding;

internal class UserStatusSeeding() : EntityWithEnumIdSeeding<UserStatus, UserStatusId>(statusId =>
    new { Id = statusId, statusId.Name, Description = GetDescription(statusId) })
{
    private static string GetDescription(UserStatusId statusId) =>
        statusId.Value == UserStatusId.Active
            ? "Active Status Description"
            : statusId.Value == UserStatusId.Inactive
                ? "Inactive Status Description" : String.Empty;
}
