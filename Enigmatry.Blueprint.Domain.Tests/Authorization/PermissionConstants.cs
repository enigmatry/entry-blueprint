using Enigmatry.Blueprint.Domain.Authorization;

namespace Enigmatry.Blueprint.Domain.Tests.Authorization;

public static class PermissionConstants
{
    public const string FirstId = "0a315dcd-2a54-4df7-b88b-af67eadb90f3";
    public const string FirstName = "First permission";

    public const string SecondId = "0b315dcd-2a54-4df7-b88b-af67eadb90f3";
    public const string SecondName = "Second permission";

    public static Permission First =>
        new PermissionBuilder()
            .WithName(FirstName)
            .WithId(new Guid(FirstId))
            .Build();

    public static Permission Second =>
        new PermissionBuilder()
            .WithName(SecondName)
            .WithId(new Guid(SecondId))
            .Build();
}
