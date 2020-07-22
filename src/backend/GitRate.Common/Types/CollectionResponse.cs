using System.Collections.Generic;

namespace GitRate.Common.Types
{
    /// <summary>
    /// Base contract for all collection requests
    /// </summary>
    public abstract class CollectionResponse<TCollection>
    {
        /// <summary>
        /// Total items count
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// List of found items
        /// </summary>
        public IEnumerable<TCollection> Items { get; set; }
    }
}
