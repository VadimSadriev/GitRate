using System;
using GitRate.Common.Extensions;

namespace GitRate.Common.Identity.Dto
{
    public class UserDto
    {
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
        
        public string Id { get; }
        public string UserName { get; }
        public string Email { get; }
    }
}