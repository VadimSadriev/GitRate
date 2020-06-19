using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GitRate.Common.Authentication
{
    public static class AuthConstants
    {
        public static class Claims
        {
            public static string UserId => ClaimTypes.NameIdentifier;
            public static string UserName => ClaimTypes.Name;
            public static string Email => ClaimTypes.Email;
            public static string JwtId => JwtRegisteredClaimNames.Jti;
            public static string JwtCreateDate => JwtRegisteredClaimNames.Iat;
        }
    }
}