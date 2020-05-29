using System;
using System.Threading;
using System.Threading.Tasks;
using Auth.Application.Commands;
using Auth.Application.Handlers;
using FluentAssertions;
using GitRate.Auth.Domain;
using GitRate.Auth.UnitTests.Base;
using GitRate.Common.Authentication;
using GitRate.Common.Identity.Types;
using Moq;
using Xunit;

namespace GitRate.Auth.UnitTests.CommandHandlers
{
    public class SignUpUserCommandHandlerTests : TestBase
    {
        private readonly SignUpUserCommandHandler _sut;
        private readonly Mock<IUserManager> _userManagerMock = new Mock<IUserManager>();
        private readonly Mock<IJwtService> _jwtServiceMock = new Mock<IJwtService>();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public SignUpUserCommandHandlerTests()
        {
            _sut = new SignUpUserCommandHandler(_userManagerMock.Object, _jwtServiceMock.Object);
        }

        [Fact]
        public async Task Should_Create_User()
        {
            // arrange
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Alice",
                Email = "Alice@gmail.com"
            };

            var password = "Alice12345";
            
            var jwtToken = new JsonWebToken(Guid.NewGuid().ToString(), "refreshToken");

            _userManagerMock
                .Setup(x => x.CreateAsync(user.UserName, user.Email, password))
                .ReturnsAsync(user.Id);

            _userManagerMock
                .Setup(x => x.GenerateRefreshToken(user.Id, jwtToken.Jti))
                .ReturnsAsync("refreshToken");
            
            _jwtServiceMock.Setup(x => x.Create(user.Id))
                .Returns(jwtToken);

            var command = new SignUpUserCommand
            {
                UserName = user.UserName,
                Email = user.Email,
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