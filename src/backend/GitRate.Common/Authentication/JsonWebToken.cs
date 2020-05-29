using System;

namespace GitRate.Common.Authentication
{
    public class JsonWebToken
    {
        public JsonWebToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException("Json web token cannot be null or empty", nameof(token));

            Token = token;
        }
        
        public string Token { get; }
    }
}