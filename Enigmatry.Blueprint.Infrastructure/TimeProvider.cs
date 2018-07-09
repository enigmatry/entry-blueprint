using System;
using Enigmatry.Blueprint.Model;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Infrastructure
{
    [UsedImplicitly]
    public class TimeProvider : ITimeProvider
    {
        private readonly Lazy<DateTimeOffset> _now = new Lazy<DateTimeOffset>(DateTimeOffset.Now);

        public DateTimeOffset Now => _now.Value;
    }
}