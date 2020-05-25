using System.Threading.Tasks;

namespace GitRate.Common.Identity.Types
{
    public interface IUserManager
    {
        public Task<string> CreateAsync(string userName, string email, string password);
    }
}