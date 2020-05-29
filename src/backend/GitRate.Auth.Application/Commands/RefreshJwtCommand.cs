using Auth.Application.Dto;
using MediatR;

namespace Auth.Application.Commands
{
    public class RefreshJwtCommand : IRequest<RefreshJwtResultDto>
    {
        public string Jwt { get; set; }
        public string RefreshToken { get; set; }
    }
}