using GitRate.Common.Identity.Dto;

namespace GitRate.Common.Identity.Types
{
    /// <summary>
    /// Service for providing user identity
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Returns current authenticated user
        /// </summary>
        UserDto GetCurrentUser();
    }
}