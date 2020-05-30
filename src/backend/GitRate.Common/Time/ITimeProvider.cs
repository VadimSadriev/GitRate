using System;

namespace GitRate.Common.Time
{
    public interface ITimeProvider
    {
        DateTimeOffset Now => DateTimeOffset.UtcNow;
        
        DateTime UnixStart => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}