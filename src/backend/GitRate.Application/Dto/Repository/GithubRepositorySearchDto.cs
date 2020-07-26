namespace GitRate.Application.Dto.Repository
{
    /// <summary>
    /// Contains data to search github resources
    /// </summary>
    public class GithubRepositorySearchDto : GithubSearchDto
    {
        /// <summary>
        /// Search criteria's to concrete search result
        /// </summary>
        public GithubRepositorySearchCriteriaDto[] Criterias { get; set; }
    }
}
