using Ardalis.SmartEnum;
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Entry.Blueprint.Core.Entities;

public abstract class EntityWithEnumId<TId> : EntityWithTypedId<TId> where TId : SmartEnum<TId>
{
    public string Name { get; private init; } = String.Empty;
}
