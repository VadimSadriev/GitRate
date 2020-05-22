using System;
using System.Threading.Tasks;
using GitRate.Auth.Domain;
using GitRate.Common.Identity.Types;
using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Services
{
    public class UserManagerService : IUserManager
    {
        private UserManager<User> _userManager;
        
        public UserManagerService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public Task<string> AddAsync(string userName, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}