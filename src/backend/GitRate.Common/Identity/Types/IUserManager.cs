using System.Threading.Tasks;
using GitRate.Common.Exceptions;
using GitRate.Common.Identity.Dto;

namespace GitRate.Common.Identity.Types
{
    /// <summary>
    /// Service for managing application users
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Find user by UserName
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        Task<UserDto> FindByUserName(string userName);
        
        /// <summary>
        /// Find user by Email
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        Task<UserDto> FindByEmail(string email);
        
        /// <summary>
        /// Creates new user in application
        /// </summary>
        /// <exception cref="AppException">Thrown if user creation was unsuccessfull</exception>
         Task<string> CreateAsync(string userName, string email, string password);

        /// <summary>
        /// Returns refresh token for given user and jwt
        /// </summary>
        Task<string> GenerateRefreshToken(string userId, string jti);
    }
}