using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Auth.Application.Commands;
using Auth.Application.Dto;
using GitRate.Common.Authentication;
using GitRate.Common.Exceptions;
using GitRate.Common.Identity.Types;
using GitRate.Common.Time;
using MediatR;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Auth.Application.Handlers
{
    public class RefreshJwtCommandHandler : IRequestHandler<RefreshJwtCommand, RefreshJwtResultDto>
    {
        private readonly IUserManager _userManager;
        private readonly IJwtService _jwtService;
        private readonly ITimeProvider _timeProvider;
        
        public RefreshJwtCommandHandler(IUserManager userManager, IJwtService jwtService, ITimeProvider timeProvider)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _timeProvider = timeProvider;
        }

        public async Task<RefreshJwtResultDto> Handle(RefreshJwtCommand request, CancellationToken cancellationToken)
        {
            var claims = _jwtService.GetClaims(request.Jwt);
            
            if (claims == null)
                throw new AppException($"Cannot get claims from jwt: {request.Jwt}.");

            var expirationClaim = claims.FindFirst(JwtRegisteredClaimNames.Exp);
            
            if (expirationClaim == null)
                throw new AppException($"Jwt {request.Jwt} does not have expiration claim.");

            var expirationDateUnix = long.Parse(expirationClaim.Value);

            var expirationUtcDateTime = _timeProvider.UnixStart.AddSeconds(expirationDateUnix);
            
            if (expirationUtcDateTime > _timeProvider.Now)
                throw new AppException("Jwt has not expired yet.");

            var jti = claims.FindFirst(JwtRegisteredClaimNames.Jti);
            
            if (jti == null)
                throw new AppException($"Jwt {request.Jwt} does no have jti claim");

            await _userManager.ExpireRefreshTokenAsync(request.RefreshToken, jti.Value, request.Jwt);

            var userDto = await _userManager.GetUserAsync(claims);

            var jwt = _jwtService.Create(userDto.Id);

            var refreshToken = await _userManager.GenerateRefreshTokenAsync(userDto.Id, jwt.Jti);

            return new RefreshJwtResultDto(jwt.Token, refreshToken);
        }
    }
}