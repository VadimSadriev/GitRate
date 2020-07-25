using GitRate.Application.Queries.Repository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GitRate.Web.Api.Controllers
{
    /// <summary>
    /// Main controller to interact with github
    /// </summary>
    [Route("api/github")]
    [Authorize]
    public class GithubController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GithubController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns popular repos
        /// </summary>
        [AllowAnonymous]
        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularRepos()
        {
            return Ok(await _mediator.Send(new GetPopularReposQuery()));
        }
    }
}
