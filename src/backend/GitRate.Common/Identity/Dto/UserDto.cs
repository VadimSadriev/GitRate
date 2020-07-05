using System;
using GitRate.Common.Extensions;

namespace GitRate.Common.Identity.Dto
{
    /// <summary>
    /// Contains user data
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Contains user data
        /// </summary>
        public UserDto(string id, string userName, string email)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("User id cannot be null or empty", nameof(id));
            
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("UserName cannot be null or empty", nameof(userName));
            
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            
            if (!email.IsEmail())
                throw new ArgumentException("Email is not in correct format", nameof(email));
            
            Id = id;
            UserName = userName;
            Email = email;
        }
        
        /// <summary>
        /// User identifier
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; }
    }
}