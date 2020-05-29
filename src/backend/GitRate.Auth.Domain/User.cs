using System;
using Microsoft.AspNetCore.Identity;

namespace GitRate.Auth.Domain
{
    /// <summary>
    /// Application user
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// User creation date
        /// </summary>
        public DateTimeOffset CreateDate { get; set; }
    }
}