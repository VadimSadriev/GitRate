using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GitRate.Common.Authentication
{
    /// <summary> Authentication constants </summary>
    public static class AuthConstants
    {
        /// <summary> Claims of authentication constants </summary>
        public static class Claims
        {
            /// <summary> Claim for user id </summary>
            public static string UserId => ClaimTypes.NameIdentifier;

            /// <summary> Claim for user name </summary>
            public static string UserName => ClaimTypes.Name;

            /// <summary> Claim for email </summary>
            public static string Email => ClaimTypes.Email;

            /// <summary> Claim for json web token </summary>
            public static string JwtId => JwtRegisteredClaimNames.Jti;

            /// <summary> Claim for json web token create date </summary>
            public static string JwtCreateDate => JwtRegisteredClaimNames.Iat;
        }
    }
}