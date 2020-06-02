using System.Threading;
using System.Threading.Tasks;
using Auth.Application.Commands;
using Auth.Application.Dto;
using GitRate.Common.Authentication;
using GitRate.Common.Extensions;
using GitRate.Common.Identity.Types;
using MediatR;

namespace Auth.Application.Handlers
{
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, SignInUserResultDto>
    {
        private readonly IUserManager _userManager;
        private readonly IJwtService _jwtService;

        public SignInUserCommandHandler(IUserManager userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            // TODO: Should consider case when you can spam signin api and generate
            // tons of jwt's. Probably should store tokens in redis and check if user is already authenticated
        }

        public async Task<SignInUserResultDto> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.UserNameOrEmail.IsEmail()
                ? await _userManager.FindByEmail(request.UserNameOrEmail)
                : await _userManager.FindByUserName(request.UserNameOrEmail);

            var jwtToken = _jwtService.Create(user.Id);

            var refreshToken = await _userManager.GenerateRefreshToken(user.Id, jwtToken.Jti);

            return new SignInUserResultDto(jwtToken.Token, refreshToken);
        }
    }
}