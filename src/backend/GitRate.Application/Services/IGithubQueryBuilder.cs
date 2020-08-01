using GitRate.Application.Dto.Repository;

namespace GitRate.Application.Services
{
    /// <summary>
    /// Service for building queries for github search
    /// </summary>
    public interface IGithubQueryBuilder
    {
        /// <summary>
        /// Builds search query for github repositories
        /// </summary>
        public string BuildRepositorySearch(GithubRepositorySearchDto searchDto);
    }
}
