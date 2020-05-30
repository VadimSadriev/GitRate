using System.Security.Claims;
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
        /// Returns user by claims 
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        Task<UserDto> GetUserAsync(ClaimsPrincipal claims);

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

        /// <summary>
        /// Expires refresh token
        /// </summary>
        /// <exception cref="EntityNotFoundException">Thrown if refresh token not found</exception>
        /// <exception cref="AppException">Thrown if token expiration failed</exception>
        Task ExpireRefreshToken(string id, string jti, string jwt);
    }
}