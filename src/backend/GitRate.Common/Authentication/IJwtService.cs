using System.Security.Claims;

namespace GitRate.Common.Authentication
{
    public interface IJwtService
    {
        JsonWebToken Create(string userId);

        ClaimsPrincipal GetClaims(string jwt);
    }
}