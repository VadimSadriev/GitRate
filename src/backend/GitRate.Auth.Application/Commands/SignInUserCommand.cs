using Auth.Application.Dto;
using MediatR;

namespace Auth.Application.Commands
{
    public class SignInUserCommand : IRequest<SignInUserResultDto>
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}