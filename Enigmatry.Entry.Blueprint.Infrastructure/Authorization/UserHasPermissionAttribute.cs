using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.AspNetCore.Authorization.Attributes;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Authorization;

public sealed class UserHasPermissionAttribute(params PermissionId[] permissions) : UserHasPermissionAttribute<PermissionId>(permissions);
