using System;

namespace Auth.Application.Dto
{
    /// <summary> Contains refresh token data </summary>
    public class RefreshJwtResultDto
    {
        /// <summary> Contains refresh token data </summary>
        public RefreshJwtResultDto(string jwt, string refreshToken)
        {
            if (string.IsNullOrEmpty(jwt))
                throw new ArgumentException("Jwt cannot be null or empty", nameof(jwt));
            
            if (string.IsNullOrEmpty(refreshToken))
                throw new ArgumentException("Refresh token cannot be null or empty", nameof(refreshToken));

            Jwt = jwt;
            RefreshToken = refreshToken;
        }
        
        /// <summary> New json web token </summary>
        public string Jwt { get; }

        /// <summary> New Refresh token for new json web token </summary>
        public string RefreshToken { get; }
    }
}