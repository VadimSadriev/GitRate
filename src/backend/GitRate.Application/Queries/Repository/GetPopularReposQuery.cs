using GitRate.Application.Dto.Repository;
using MediatR;

namespace GitRate.Application.Queries.Repository
{
    /// <summary>
    /// Query to return popular repos by stars or some other staff
    /// </summary>
    public class GetPopularReposQuery : IRequest<SearchRepositoryResponseDto>
    {
    }
}
