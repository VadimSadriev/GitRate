using GitRate.Common.Identity.Dto;
using GitRate.Common.Identity.Types;
using GitRate.Web.Common.Contracts.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GitRate.Web.Api.Controllers
{
    /// <summary>
    /// Controller to work with user information
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        /// <summary>
        /// Controller to work with user information
        /// </summary>
        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        /// <summary>
        /// Returns current user's information
        /// </summary>
        [HttpGet("current")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ExceptionContract))]
        public IActionResult GetCurrent()
        {
            return Ok(_identityService.GetCurrentUser());
        }
    }
}
