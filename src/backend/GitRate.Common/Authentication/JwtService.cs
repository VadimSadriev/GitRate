using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GitRate.Common.Time;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GitRate.Common.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly ITimeProvider _time;
        private readonly JwtOptions _jwtOptions;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ILogger<JwtService> _logger;

        public JwtService(ITimeProvider time, IOptions<JwtOptions> jwtOptions, TokenValidationParameters tokenValidationParameters, ILogger<JwtService> logger)
        {
            _time = time;
            _jwtOptions = jwtOptions.Value;
            _tokenValidationParameters = tokenValidationParameters;
            _logger = logger;
        }

        public JsonWebToken Create(string userId, List<Claim> customClaims = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId cannot be null or whitespace.", nameof(userId));

            var now = _time.Now;

            var jti = Guid.NewGuid().ToString();
            
            var claims = new List<Claim>
            {
                new Claim(AuthConstants.Claims.JwtId, jti),
                new Claim(AuthConstants.Claims.JwtCreateDate, now.ToString()),
                new Claim(AuthConstants.Claims.UserId, userId)
            };
            
            if (customClaims != null)
                claims.AddRange(customClaims);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: now.AddMinutes(_jwtOptions.Expires).UtcDateTime
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken(jti, token);
        }

        public ClaimsPrincipal? GetClaims(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            try
            {
                var principal = tokenHandler.ValidateToken(jwt, _tokenValidationParameters, out var validatedToken);

                return !IsValidJwtAlgorithm(validatedToken) ? null : principal;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Could not get claims from jwt: {jwt}");
                return null;
            }
        }

        private bool IsValidJwtAlgorithm(SecurityToken validatedToken)
        {
            return validatedToken is JwtSecurityToken jwtSecurityToken
                   && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, System.StringComparison.InvariantCultureIgnoreCase);
        }
    }
}