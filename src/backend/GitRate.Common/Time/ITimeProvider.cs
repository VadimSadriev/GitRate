using System;

namespace GitRate.Common.Time
{
    public interface ITimeProvider
    {
        DateTimeOffset Now => DateTimeOffset.UtcNow;
    }
}