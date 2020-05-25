using System.Threading.Tasks;

namespace GitRate.Common.Authentication
{
    public interface IJwtService
    {
        JsonWebToken Create(string userId);
    }
}