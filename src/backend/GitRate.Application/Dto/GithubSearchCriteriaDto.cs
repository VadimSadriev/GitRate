using System;

namespace GitRate.Application.Dto
{
    /// <summary>
    /// Criteria to concrete search for github resource
    /// for example search in readme or description
    /// </summary>
    public class GithubSearchCriteriaDto<TCriteria>
        where TCriteria : Enum
    {
        /// <summary>
        /// Criteria to concrete search
        /// </summary>
        public TCriteria Criteria { get; set; }

        /// <summary>
        /// Values for this criteria
        /// </summary>
        public string[] Values { get; set; }
    }
}
