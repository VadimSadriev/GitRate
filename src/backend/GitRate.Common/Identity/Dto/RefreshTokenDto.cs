using System;

namespace GitRate.Common.Identity.Dto
{
    public class RefreshTokenDto
    {
        public RefreshTokenDto(string id, string jti, bool isUsed, DateTimeOffset expireDate)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Refresh token id cannot be null or empty", nameof(id));
            
            if (string.IsNullOrEmpty(jti))
                throw new ArgumentException("Jwt id cannot be null or empty", nameof(jti));

            Id = id;
            Jti = jti;
            IsUsed = isUsed;
            ExpireDate = expireDate;
        }
        
        /// <summary>
        /// Refresh token id
        /// </summary>
        public string Id { get; }
        
        /// <summary>
        /// Jwt id refresh token for
        /// </summary>
        public string Jti { get; }
        
        /// <summary>
        /// Flag if refresh token has been already used
        /// </summary>
        public bool IsUsed { get; }
        
        /// <summary>
        /// Refresh token expiration date
        /// </summary>
        public DateTimeOffset ExpireDate { get; }
    }
}