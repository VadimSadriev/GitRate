using System.Threading.Tasks;

namespace GitRate.Common.Authentication
{
    public interface IJwtService
    {
        Task<JsonWebToken> Create(string userId);
    }
}