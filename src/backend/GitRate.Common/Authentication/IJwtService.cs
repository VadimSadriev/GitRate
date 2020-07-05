using System.Collections.Generic;
using System.Security.Claims;

namespace GitRate.Common.Authentication
{
    /// <summary> Service for work with json web token </summary>
    public interface IJwtService
    {
        /// <summary> Creates new json web token </summary>
        JsonWebToken Create(string userId, List<Claim> customClaims = null);

        /// <summary> Gets claims from passed json web token </summary>
        ClaimsPrincipal? GetClaims(string jwt);
    }
}