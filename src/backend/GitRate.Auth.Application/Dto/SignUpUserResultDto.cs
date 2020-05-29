using System;

namespace Auth.Application.Dto
{
    public class SignUpUserResultDto
    {
        public SignUpUserResultDto(string jwt, string refreshToken)
        {
            if (string.IsNullOrEmpty(jwt))
                throw new ArgumentException("Json web token cannot be null or empty", nameof(jwt));
            
            if (string.IsNullOrEmpty(refreshToken))
                throw new ArgumentException("Refresh token cannot be null or empty", nameof(refreshToken));
            
            Jwt = jwt;
            RefreshToken = refreshToken;
        }
        
        public string Jwt { get; }
        
        public string RefreshToken { get; }
    }
}