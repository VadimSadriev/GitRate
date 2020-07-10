using System;

namespace GitRate.Common.Time
{
    /// <summary>
    /// Service for providing time
    /// </summary>
    public interface ITimeProvider
    {
        /// <summary>
        /// Current machine time in utc
        /// </summary>
        DateTimeOffset Now => DateTimeOffset.UtcNow;
        
        /// <summary>
        /// Unix start date
        /// </summary>
        DateTime UnixStart => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}