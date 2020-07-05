using Auth.Application.Dto;
using MediatR;

namespace Auth.Application.Commands
{
    /// <summary> Command to create new user account </summary>
    public class SignUpUserCommand : IRequest<SignUpUserResultDto>
    {
        /// <summary> User name </summary>
        public string UserName { get; set; }

        /// <summary> Email </summary>
        public string Email { get; set; }

        /// <summary> Password </summary>
        public string Password { get; set; }
    }
}