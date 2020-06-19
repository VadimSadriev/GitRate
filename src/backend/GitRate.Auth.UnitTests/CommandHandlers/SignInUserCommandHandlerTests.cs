using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Auth.Application.Commands;
using Auth.Application.Handlers;
using FluentAssertions;
using FluentValidation.Validators;
using GitRate.Auth.Domain;
using GitRate.Auth.UnitTests.Base;
using GitRate.Common.Authentication;
using GitRate.Common.Extensions;
using GitRate.Common.Identity.Dto;
using GitRate.Common.Identity.Types;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GitRate.Auth.UnitTests.CommandHandlers
{
    public class SignInUserCommandHandlerTests : TestBase
    {
        private SignInUserCommandHandler _sut;
        private readonly Mock<IUserManager> _userManagerMock = new Mock<IUserManager>();
        private readonly Mock<IJwtService> _jwtServiceMock = new Mock<IJwtService>();
        private readonly Mock<ILogger<SignInUserCommandHandler>> _loggerMock = new Mock<ILogger<SignInUserCommandHandler>>();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public SignInUserCommandHandlerTests()
        {
            _sut = new SignInUserCommandHandler(_userManagerMock.Object, _jwtServiceMock.Object, _loggerMock.Object);
        }

        [Theory]
        [InlineData("Alice")]
        [InlineData("Alice@gmail.com")]
        public async Task Should_SignIn(string userNameOrEmail)
        {
            // arrange
            var userDto = new UserDto(Guid.NewGuid().ToString(), "Alice", "Alice@gmail.com");

            var password = "Alice12345";

            var jwtToken = new JsonWebToken(Guid.NewGuid().ToString(), "refreshToken");
            
            _userManagerMock
                .Setup(x => x.FindByEmailAsync(userNameOrEmail))
                .ReturnsAsync(userDto);
            
            _userManagerMock
                .Setup(x => x.FindByUserNameAsync(userNameOrEmail))
                .ReturnsAsync(userDto);

            _userManagerMock.Setup(x => x.VerifyPasswordAsync(userDto.Id, password))
                .ReturnsAsync(true);

            _userManagerMock
                .Setup(x => x.GenerateRefreshTokenAsync(userDto.Id, jwtToken.Jti))
                .ReturnsAsync("refreshToken");
            
            _jwtServiceMock.Setup(x => x.Create(userDto.Id, null))
                .Returns(jwtToken);
            
            var command = new SignInUserCommand
            {
                UserNameOrEmail = userNameOrEmail,
                Password = password
            };

            // act
            var result = await _sut.Handle(command, _cancellationTokenSource.Token);

            // assert
            result.Should().NotBeNull();
            result.Jwt.Should().Be(jwtToken.Token);
        }
    }
}