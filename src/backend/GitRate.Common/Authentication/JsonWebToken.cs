using System;

namespace GitRate.Common.Authentication
{
    public class JsonWebToken
    {
        public JsonWebToken(string jti, string token)
        {
            if (string.IsNullOrEmpty(jti))
                throw new ArgumentException("Jti cannot be null or empty", nameof(jti));
            
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException("Json web token cannot be null or empty", nameof(token));

            Token = token;
        }
        
        
        public string Jti { get; }
        public string Token { get; }
    }
}