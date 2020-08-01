using GitRate.Application.Enums;
using System;

namespace GitRate.Application.Dto
{
    /// <summary>
    /// Contains data to search github resources
    /// </summary>
    public abstract class GithubSearchDto<TRepositorySearchCriteria>
         where TRepositorySearchCriteria : Enum
    {
        /// <summary>
        /// Keyword to search
        /// </summary>
        public string[] Keywords { get; set; }

        /// <summary>
        /// Flag if search should find only on resource
        /// </summary>
        public bool IsSingle { get; set; }

        /// <summary>
        /// Sort by stars, create date etc...
        /// </summary>
        public string Sort { get; set; }

        public string Order { get; set; }

        /// <summary>
        /// Search criteria's to concrete search result
        /// </summary>
        public GithubSearchCriteriaDto<TRepositorySearchCriteria>[] Criterias { get; set; }
    }
}
