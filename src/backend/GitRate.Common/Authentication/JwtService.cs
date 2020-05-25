using System.Threading.Tasks;

namespace GitRate.Common.Authentication
{
    public class JwtService : IJwtService
    {
        public Task<JsonWebToken> Create(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}