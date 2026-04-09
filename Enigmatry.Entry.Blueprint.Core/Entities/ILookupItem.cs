namespace Enigmatry.Entry.Blueprint.Core.Entities;

public interface ILookupItem<out TId>
{
    public TId Id { get; }
    public string Name { get; }
}
