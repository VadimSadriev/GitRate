using System;

namespace GitRate.Auth.Domain
{
    /// <summary>
    /// Token used to refresh authentication json web token
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// Refresh token identifier
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Json web token identifier
        /// </summary>
        public string JwtId { get; set; }
        
        /// <summary>
        /// Refresh token creation date
        /// </summary>
        public DateTimeOffset CreateDate { get; set; }
        
        /// <summary>
        /// Refresh token expiration date
        /// </summary>
        public DateTimeOffset ExpireDate { get; set; }
        public bool IsUsed { get; set; }
        
        /// <summary>
        /// User identifier who owns this refresh token
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// User who this token belongs to
        /// </summary>
        public User User { get; set; }
    }
}