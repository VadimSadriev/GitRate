using System;
using System.Linq;
using System.Threading.Tasks;
using GitRate.Auth.Domain;
using GitRate.Common.Exceptions;
using GitRate.Common.Identity.Dto;
using GitRate.Common.Identity.Types;
using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Services
{
    public class UserManagerService : IUserManager
    {
        private readonly UserManager<User> _userManager;
        
        public UserManagerService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Find user by UserName
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        public Task<UserDto> FindByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find user by Email
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        public Task<UserDto> FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates new user in application
        /// </summary>
        /// <exception cref="AppException">Thrown if user creation was unsuccessfull</exception>
        public async Task<string> CreateAsync(string userName, string email, string password)
        {
            var user = new User
            {
                UserName = userName,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new AppException(result.Errors.Select(x => x.Description).Aggregate((a, b) => $"{a}{Environment.NewLine}{b}"));

            return user.Id;
        }
    }
}