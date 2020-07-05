using GitRate.Common.Authentication;
using GitRate.Common.Identity.Dto;
using GitRate.Common.Identity.Types;
using Microsoft.AspNetCore.Http;
using System.Linq;

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

            var identities = _httpContext.User?.Identities;

            if (identities == null)
                return null;

            var claims = identities.First();

            var id = claims.FindFirst(AuthConstants.Claims.UserId)?.Value;
            var userName = claims.FindFirst(AuthConstants.Claims.UserName)?.Value;
            var email = claims.FindFirst(AuthConstants.Claims.Email)?.Value;

            _user = new UserDto(id, userName, email);

            return _user;
        }
    }
}