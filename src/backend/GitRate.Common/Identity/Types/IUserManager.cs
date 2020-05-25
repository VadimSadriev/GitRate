using System.Threading.Tasks;

namespace GitRate.Common.Identity.Types
{
    public interface IUserManager
    {
        /// <summary>
        /// Creates new user in application
        /// </summary>
        /// <exception cref="AppException">Thrown if user creation was unsuccessfull</exception>
        public Task<string> CreateAsync(string userName, string email, string password);
    }
}