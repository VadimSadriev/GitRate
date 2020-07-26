using GitRate.Application.Enums;

namespace GitRate.Application.Dto.Repository
{
    /// <summary>
    /// Criteria to concrete search for github repositories
    /// for example search in readme or description
    /// </summary>
    public class GithubRepositorySearchCriteriaDto : GithubSearchCriteriaDto<RepositorySearchCriteria>
    {
    }
}
