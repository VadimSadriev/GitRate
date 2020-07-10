using System;
using System.Linq;
using System.Security.Claims;
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

        /// <summary>
        /// Service for managing application users
        /// </summary>
        public UserManagerService(
            UserManager<User> userManager,
            ITimeProvider timeProvider,
            AuthContext context,
            ILogger<UserManagerService> logger)
        {
            _userManager = userManager;
            _timeProvider = timeProvider;
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Returns user by claims 
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        public async Task<UserDto> GetUserAsync(ClaimsPrincipal claims)
        {
            var user = await _userManager.GetUserAsync(claims);

            if (user != null)
                return new UserDto(user.Id, user.UserName, user.Email);

            _logger.LogError("User not found for passed claims @{claims}", claims);
            throw new EntityNotFoundException($"User not found");
        }

        /// <summary>
        /// Find user by UserName
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        public async Task<UserDto> FindByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName)
                       ?? throw new EntityNotFoundException($"User with UserName: {userName} not found");

            return new UserDto(user.Id, user.UserName, user.Email);
        }

        /// <summary>
        /// Find user by Email
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        public async Task<UserDto> FindByEmailAsync(string email)
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
                throw new AppException(result.Errors.Select(x => x.Description)
                    .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}"));

            return user.Id;
        }

        /// <summary>
        /// Checks if password if valid for given user
        /// </summary>
        public async Task<bool> VerifyPasswordAsync(string userId, string password)
        {
            var user = await _userManager.FindByIdAsync(userId)
                       ?? throw new EntityNotFoundException($"User with Id: {userId} not found");

            return await _userManager.CheckPasswordAsync(user, password);
        }

        /// <summary>
        /// Returns refresh token for given user and jwt
        /// </summary>
        public async Task<string> GenerateRefreshTokenAsync(string userId, string jti)
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
                _logger.LogError(ex,
                    $"Error occured during refresh token creation for userId: {userId} and jti: {jti}");
                throw new AppException($"Cannot create refresh token for userId: {userId} and jti: {jti}", ex);
            }

            return refreshToken.Id;
        }

        /// <summary>
        /// Expires refresh token
        /// </summary>
        /// <exception cref="EntityNotFoundException">Thrown if refresh token not found</exception>
        /// <exception cref="AppException">Thrown if token expiration failed</exception>
        public async Task ExpireRefreshTokenAsync(string id, string jti, string jwt)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Id == id)
                               ?? throw new EntityNotFoundException($"Refresh token with id: {id} not found");

            if (_timeProvider.Now > refreshToken.ExpireDate)
                throw new AppException($"Refresh token: {id} has been expired");

            if (refreshToken.IsUsed)
                throw new AppException($"Refresh token: {id} has been used");

            if (refreshToken.Jti != jti)
                throw new AppException($"Refresh token: {id} does not belong to jwt: {jwt}");

            try
            {
                refreshToken.IsUsed = true;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Could not update refresh token with id: {id}");
                throw new AppException("An error occurred during refreshing token", ex);
            }
        }
    }
}