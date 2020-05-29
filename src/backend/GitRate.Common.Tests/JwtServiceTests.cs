using System;
using FluentAssertions;
using GitRate.Common.Authentication;
using GitRate.Common.Time;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit;

namespace GitRate.Common.Tests
{
    public class JwtServiceTests
    {
        private readonly JwtService _sut;
        private readonly Mock<ILogger<JwtService>> _loggerMock = new Mock<ILogger<JwtService>>();

        public JwtServiceTests()
        {
            var jwtOptions = new JwtOptions
            {
                Audience = "GitRate",
                Issuer = "GitRate.Server",
                Expires = 5,
                SecretKey = "YouWillNeverGuessMySecretKey"
            };
            
            _sut = new JwtService(new TimeProvider(), jwtOptions, new TokenValidationParameters(), _loggerMock.Object);
        }

        [Fact]
        public void Should_Generate_Jwt()
        {
            // arrange
           
            // act
            var result = _sut.Create(Guid.NewGuid().ToString());

            result.Should().NotBeNull();
            result.Token.Should().NotBeNullOrEmpty();
        }
    }
}