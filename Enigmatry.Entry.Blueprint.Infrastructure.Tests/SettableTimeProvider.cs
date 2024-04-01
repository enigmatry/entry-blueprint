using Enigmatry.Entry.Core;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Tests;

public class SettableTimeProvider : ITimeProvider
{
    private DateTimeOffset? _nowValue;
    private readonly Lazy<DateTimeOffset> _now = new(() => DateTimeOffset.UtcNow);

    public DateTimeOffset FixedUtcNow => _nowValue ?? _now.Value;

    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

    public void SetNow(DateTimeOffset now) =>
        _nowValue = now;
}
