using System;

namespace Enigmatry.Blueprint.Model
{
    public interface ITimeProvider
    {
        DateTimeOffset Now { get; }
    }
}
