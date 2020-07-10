using System;

namespace Auth.Application.Dto
{
    /// <summary> Contains data with user creation result </summary>
    public class SignUpUserResultDto
    {
        /// <summary> Contains data with user creation result </summary>
        public SignUpUserResultDto(string jwt, string refreshToken)
        {
            if (string.IsNullOrEmpty(jwt))
                throw new ArgumentException("Json web token cannot be null or empty", nameof(jwt));
            
            if (string.IsNullOrEmpty(refreshToken))
                throw new ArgumentException("Refresh token cannot be null or empty", nameof(refreshToken));
            
            Jwt = jwt;
            RefreshToken = refreshToken;
        }
        
        /// <summary> Json web token for newly created user </summary>
        public string Jwt { get; }

        /// <summary> Refresh token for json web token </summary>
        public string RefreshToken { get; }
    }
}