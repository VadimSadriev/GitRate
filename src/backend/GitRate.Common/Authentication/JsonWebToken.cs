using System;

namespace GitRate.Common.Authentication
{
    /// <summary> Contains data for newly created json web token </summary>
    public class JsonWebToken
    {
        /// <summary> Contains data for newly created json web token </summary>
        public JsonWebToken(string jti, string token)
        {
            if (string.IsNullOrEmpty(jti))
                throw new ArgumentException("Jti cannot be null or empty", nameof(jti));
            
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException("Json web token cannot be null or empty", nameof(token));

            Jti = jti;
            Token = token;
        }
        
        /// <summary> Json web token identifier </summary>
        public string Jti { get; }

        /// <summary> Actual json web token </summary>
        public string Token { get; }
    }
}