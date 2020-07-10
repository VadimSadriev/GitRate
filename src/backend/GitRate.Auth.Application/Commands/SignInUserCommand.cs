using Auth.Application.Dto;
using MediatR;

namespace Auth.Application.Commands
{
    /// <summary> Command to login user in system </summary>
    public class SignInUserCommand : IRequest<SignInUserResultDto>
    {
        /// <summary> User name or email to log in </summary>
        public string UserNameOrEmail { get; set; }

        /// <summary> User's password </summary>
        public string Password { get; set; }
    }
}