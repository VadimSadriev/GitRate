namespace GitRate.Common.Authentication
{
    /// <summary>
    /// Options used for jwt authentication
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Secret key used to generate jwt
        /// </summary>
        public string SecretKey { get; set; }
        
        /// <summary>
        /// Who generates token
        /// </summary>
        public string Issuer { get; set; }
        
        /// <summary>
        /// Who recives token
        /// </summary>
        public string Audience { get; set; }
        
        /// <summary>
        /// Expire time in milliseconds
        /// </summary>
        public int Expires { get; set; }
    }
}