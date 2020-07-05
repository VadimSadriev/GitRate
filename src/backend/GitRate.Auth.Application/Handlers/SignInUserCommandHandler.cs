using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Auth.Application.Commands;
using Auth.Application.Dto;
using GitRate.Common.Authentication;
using GitRate.Common.Exceptions;
using GitRate.Common.Extensions;
using GitRate.Common.Identity.Types;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Auth.Application.Handlers
{
    /// <summary>
    /// Handles <see cref="SignInUserCommand"/>
    /// </summary>
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, SignInUserResultDto>
    {
        private readonly IUserManager _userManager;
        private readonly IJwtService _jwtService;
        private readonly ILogger<SignInUserCommandHandler> _logger;

        /// <summary>
        /// Handles <see cref="SignInUserCommand"/>
        /// </summary>
        public SignInUserCommandHandler(IUserManager userManager, IJwtService jwtService, ILogger<SignInUserCommandHandler> logger)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _logger = logger;
            // TODO: Should consider case when you can spam signin api and generate
            // tons of jwt's. Probably should store tokens in redis and check if user is already authenticated
        }

        /// <summary>
        /// Logs in new user
        /// </summary>
        public async Task<SignInUserResultDto> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = request.UserNameOrEmail.IsEmail()
                    ? await _userManager.FindByEmailAsync(request.UserNameOrEmail)
                    : await _userManager.FindByUserNameAsync(request.UserNameOrEmail);

                var isPasswordValid = await _userManager.VerifyPasswordAsync(user.Id, request.Password);

                if (!isPasswordValid)
                {
                    _logger.LogError($"Password: {request.Password} is not valid for user with id: {user.Id}");
                    throw new AppException();
                }
                
                var customClaims = new List<Claim>
                {
                    new Claim(AuthConstants.Claims.UserName, user.UserName),
                    new Claim(AuthConstants.Claims.Email, user.Email)
                };

                var jwtToken = _jwtService.Create(user.Id);

                var refreshToken = await _userManager.GenerateRefreshTokenAsync(user.Id, jwtToken.Jti);

                return new SignInUserResultDto(jwtToken.Token, refreshToken);
            }
            catch (Exception ex) when (ex is EntityNotFoundException || ex is AppException)
            {
                // do not tell client what exactly is wrong
                throw new AppException("Incorrect UserName, Email or Password");
            }
        }
    }
}