using System.Collections.Generic;
using System.Security.Claims;

namespace GitRate.Common.Authentication
{
    public interface IJwtService
    {
        JsonWebToken Create(string userId, List<Claim> customClaims = null);

        ClaimsPrincipal? GetClaims(string jwt);
    }
}