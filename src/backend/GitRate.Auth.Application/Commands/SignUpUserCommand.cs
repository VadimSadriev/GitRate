using Auth.Application.Dto;
using MediatR;

namespace Auth.Application.Commands
{
    public class SignUpUserCommand : IRequest<SignUpUserResultDto>
    {
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}