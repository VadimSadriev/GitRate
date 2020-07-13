using Auth.Application.Commands;
using Auth.Application.Dto;
using GitRate.Web.Common.Contracts.Exception;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GitRate.Auth.Web.Controllers
{
    /// <summary> Main auth controller for user identity </summary>
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary> Main auth controller for user identity </summary>
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary> Creates new user account if no such exists </summary>
        [HttpPost]
        [Route("signup")]
        [ProducesResponseType(typeof(SignUpUserResultDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ExceptionContract))]
        public async Task<IActionResult> SignUp([FromBody] SignUpUserCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary> Authenticates user in system </summary>
        [HttpPost("signin")]
        [ProducesResponseType(typeof(SignInUserResultDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ExceptionContract))]
        public async Task<IActionResult> SignIn([FromBody] SignInUserCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary> Refreshes expired json web token </summary>
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(RefreshJwtResultDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ExceptionContract))]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshJwtCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}