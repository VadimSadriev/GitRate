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
    public class GetPopularReposQueryHandler : IRequestHandler<GetPopularReposQuery, PopularReposResponseDto>
    {
        /// <summary>
        /// Handles <see cref="GetPopularReposQuery"/> and returns <see cref="PopularReposResponseDto"/>
        /// </summary>
        public async Task<PopularReposResponseDto> Handle(GetPopularReposQuery request, CancellationToken cancellationToken)
        {
            return new PopularReposResponseDto
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
