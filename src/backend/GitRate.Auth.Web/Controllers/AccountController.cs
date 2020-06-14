using System.Threading.Tasks;
using Auth.Application.Commands;
using Auth.Application.Dto;
using GitRate.Web.Common.Contracts.Exception;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GitRate.Auth.Web.Controllers
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public AccountController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }
        
        [HttpPost]
        [Route("signup")]
        [ProducesResponseType(typeof(SignUpUserResultDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ExceptionContract))]
        public async Task<IActionResult> SignUp([FromBody] SignUpUserCommand command)
        {
            var result = await _mediator.Send(command);
        
            return Ok(result);
        }

        [HttpPost]
        [Route("signin")]
        [ProducesResponseType(typeof(SignInUserResultDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ExceptionContract))]
        public async Task<IActionResult> SignIn([FromBody] SignInUserCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [Route("refresh-token")]
        [ProducesErrorResponseType(typeof(ExceptionContract))]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshJwtCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("env")]
        public async Task<IActionResult> Env()
        {
            return Ok(_env.EnvironmentName);
        }
    }
}