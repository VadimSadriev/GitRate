using System.Threading.Tasks;

namespace GitRate.Common.Identity.Types
{
    public interface IUserManager
    {
        public Task<string> AddAsync(string userName, string email, string password);
    }
}