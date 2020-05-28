using System;

namespace GitRate.Common.Identity.Dto
{
    public class UserDto
    {
        public UserDto(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("User id cannot be null or empty", nameof(id));
            
            Id = id;
        }
        
        public string Id { get; }
    }
}