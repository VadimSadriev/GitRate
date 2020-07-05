using System;

namespace Auth.Application.Dto
{
    /// <summary> Contains result data of login process </summary>
    public class SignInUserResultDto
    {
        /// <summary> Contains result data of login process </summary>
        public SignInUserResultDto(string jwt, string refreshToken)
        {
            if (string.IsNullOrEmpty(jwt))
                throw new ArgumentException("Jwt cannot be null or empty", nameof(jwt));
            
            if (string.IsNullOrEmpty(refreshToken))
                throw new ArgumentException("Refresh token cannot be null or empty", nameof(refreshToken));
            
            Jwt = jwt;
            RefreshToken = refreshToken;
        }

        /// <summary> Json web token </summary>
        public string Jwt { get; }

        /// <summary> Refresh token for json web token </summary>
        public string RefreshToken { get; }

    }
}