using GitRate.Common.Identity.Dto;
using GitRate.Common.Identity.Types;
using Microsoft.AspNetCore.Http;

namespace GitRate.Web.Common.Services
{
    /// <summary>
    /// Service for providing user identity
    /// </summary>
    public class HttpContextIdentityService : IIdentityService
    {
        private readonly HttpContext _httpContext;
        private UserDto _user;
        
        /// <summary>
        /// Service for providing user identity
        /// </summary>
        public HttpContextIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }
        
        /// <summary>
        /// Returns current authenticated user
        /// </summary>
        public UserDto GetCurrentUser()
        {
            if (_user != null)
                return _user;

            var claims = _httpContext.User?.Claims;

            if (claims == null)
                return null;

            return _user;
        }
    }
}