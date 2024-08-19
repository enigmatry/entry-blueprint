using Enigmatry.Entry.Blueprint.Domain.Users;
using JetBrains.Annotations;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Data.Configurations;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class UserStatusConfiguration : EntityWithEnumIdConfiguration<UserStatus, UserStatusId>;
