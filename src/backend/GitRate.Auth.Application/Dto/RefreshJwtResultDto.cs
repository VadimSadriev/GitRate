using System;

namespace Auth.Application.Dto
{
    public class RefreshJwtResultDto
    {
        public RefreshJwtResultDto(string jwt, string refreshToken)
        {
            if (string.IsNullOrEmpty(jwt))
                throw new ArgumentException("Jwt cannot be null or empty", nameof(jwt));
            
            if (string.IsNullOrEmpty(refreshToken))
                throw new ArgumentException("Refresh token cannot be null or empty", nameof(refreshToken));

            Jwt = jwt;
            RefreshToken = refreshToken;
        }
        
        public string Jwt { get; }
        public string RefreshToken { get; }
    }
}