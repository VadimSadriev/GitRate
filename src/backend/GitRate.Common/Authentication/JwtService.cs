using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GitRate.Common.Time;
using Microsoft.IdentityModel.Tokens;

namespace GitRate.Common.Authentication
{
    public class JwtService : IJwtService
    {
        private ITimeProvider _time;
        private JwtOptions _jwtOptions;
        
        public JwtService(ITimeProvider time, JwtOptions jwtOptions)
        {
            _time = time;
            _jwtOptions = jwtOptions;
        }
        
        public JsonWebToken Create(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId cannot be null or whitespace.", nameof(userId));

            var now = _time.Now;
            
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId), 
            };
            
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.Sha256);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: now.AddMinutes(_jwtOptions.Expires).DateTime
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                Token = token
            };
        }
    }
}