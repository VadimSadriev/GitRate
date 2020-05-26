using System.Threading.Tasks;
using Auth.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GitRate.Auth.Web.Controllers
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpUserCommand command)
        {
            var result = await _mediator.Send(command);
        
            return Ok(result);
        }
    }
}