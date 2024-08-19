using Enigmatry.Entry.Blueprint.Core.Entities;
using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.Domain.Users;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class UserStatus : EntityWithEnumId<UserStatusId>
{
    public string Description { get; private set; } = String.Empty;
};
