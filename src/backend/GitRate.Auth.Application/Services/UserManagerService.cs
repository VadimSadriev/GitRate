using System;
using System.Linq;
using System.Threading.Tasks;
using GitRate.Auth.Domain;
using GitRate.Auth.Persistence;
using GitRate.Common.Exceptions;
using GitRate.Common.Identity.Dto;
using GitRate.Common.Identity.Types;
using GitRate.Common.Time;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Auth.Application.Services
{
    /// <summary>
    /// Service for managing application users
    /// </summary>
    public class UserManagerService : IUserManager
    {
        private readonly UserManager<User> _userManager;
        private readonly ITimeProvider _timeProvider;
        private readonly AuthContext _context;
        private readonly ILogger<UserManagerService> _logger;

        public UserManagerService(UserManager<User> userManager, ITimeProvider timeProvider, AuthContext context, ILogger<UserManagerService> logger)
        {
            _userManager = userManager;
            _timeProvider = timeProvider;
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Find user by UserName
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        public async Task<UserDto> FindByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName)
                       ?? throw new EntityNotFoundException($"User with UserName: {userName} not found");

            return new UserDto(user.Id, user.UserName, user.Email);
        }

        /// <summary>
        /// Find user by Email
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        public async Task<UserDto> FindByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email)
                       ?? throw new EntityNotFoundException($"User with Email: {email} not found");

            return new UserDto(user.Id, user.UserName, user.Email);
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

        /// <summary>
        /// Returns refresh token for given user and jwt
        /// </summary>
        public async Task<string> GenerateRefreshToken(string userId, string jti)
        {
            var refreshToken = new RefreshToken
            {
                Jti = jti,
                UserId = userId,
                ExpireDate = _timeProvider.Now.AddMonths(1)
            };

            try
            {
                _context.RefreshTokens.Add(refreshToken);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Error occured during refresh token creation for userId: {userId} and jti: {jti}");
                throw new AppException($"Cannot create refresh token for userId: {userId} and jti: {jti}", ex);
            }

            return refreshToken.Id;
        }
    }
}