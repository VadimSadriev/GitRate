using GitRate.Application.Dto.Repository;
using GitRate.Application.Github;
using GitRate.Application.Queries.Repository;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GitRate.Application.Handlers.Repository
{
    /// <summary>
    /// Handles <see cref="GetPopularReposQuery"/>
    /// </summary>
    public class GetPopularReposQueryHandler : IRequestHandler<GetPopularReposQuery, SearchRepositoryResponseDto>
    {
        /// <summary>
        /// Handles <see cref="GetPopularReposQuery"/> and returns <see cref="SearchRepositoryResponseDto"/>
        /// </summary>
        public async Task<SearchRepositoryResponseDto> Handle(GetPopularReposQuery request, CancellationToken cancellationToken)
        {
            return new SearchRepositoryResponseDto
            {
                TotalCount = 1,
                Items = new List<RepoItemDto>
                   {
                       new RepoItemDto { Name = "Fake repository" }
                   }
            };
        }
    }
}
