using System;
using System.Threading;
using System.Threading.Tasks;
using Auth.Application.Commands;
using Auth.Application.Dto;
using GitRate.Auth.Persistence;
using GitRate.Common.Authentication;
using GitRate.Common.Identity.Types;
using MediatR;

namespace Auth.Application.Handlers
{
    public class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand, SignUpUserResultDto>
    {
        private readonly IUserManager _userManager;
        private readonly IJwtService _jwtService;
        
        public SignUpUserCommandHandler(IUserManager userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }
        
        public async Task<SignUpUserResultDto> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userManager.CreateAsync(request.UserName, request.Email, request.Password);

            var jwtToken = _jwtService.Create(userId);

            return new SignUpUserResultDto
            {
                JwtToken = jwtToken.Token
            };
        }
    }
}