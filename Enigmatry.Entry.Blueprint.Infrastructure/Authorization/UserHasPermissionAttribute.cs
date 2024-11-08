using Enigmatry.Entry.AspNetCore.Authorization.Attributes;
using Enigmatry.Entry.Blueprint.Domain.Authorization;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Authorization;

public sealed class UserHasPermissionAttribute(params PermissionId[] permissions) : UserHasPermissionAttribute<PermissionId>(permissions);
