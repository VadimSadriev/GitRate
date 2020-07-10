using Auth.Application.Dto;
using MediatR;

namespace Auth.Application.Commands
{
    /// <summary> Command to refresh expired json web token </summary>
    public class RefreshJwtCommand : IRequest<RefreshJwtResultDto>
    {
        /// <summary> Expired json web token </summary>
        public string Jwt { get; set; }

        /// <summary> Refresh token to refresh expired json web token </summary>
        public string RefreshToken { get; set; }
    }
}