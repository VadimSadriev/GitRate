using System;
using System.Security.Claims;
using System.Text;
using FluentAssertions;
using GitRate.Common.Authentication;
using GitRate.Common.Time;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit;

namespace GitRate.Common.UnitTests
{
    public class JwtServiceTests
    {
        private readonly JwtService _sut;
        private readonly Mock<ILogger<JwtService>> _loggerMock = new Mock<ILogger<JwtService>>();
        private readonly Mock<IOptions<JwtOptions>> _jwtOptionsMock = new Mock<IOptions<JwtOptions>>();

        public JwtServiceTests()
        {
            var jwtOptions = new JwtOptions
            {
                Audience = "GitRate",
                Issuer = "GitRate.Server",
                Expires = 5,
                SecretKey = "YouWillNeverGuessMySecretKey"
            };

            _jwtOptionsMock.Setup(x => x.Value)
                .Returns(jwtOptions);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
            };

            _sut = new JwtService(new TimeProvider(), _jwtOptionsMock.Object, tokenValidationParameters, _loggerMock.Object);
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

        [Fact]
        public void Should_Return_Claims()
        {
            // arrange
            var userId = Guid.NewGuid().ToString();

            var jwt = _sut.Create(userId);

            // act
            var claims = _sut.GetClaims(jwt.Token);

            // assert
            claims.Should().NotBeNull();
            var jtiClaim = claims.FindFirst(JwtRegisteredClaimNames.Jti);
            jtiClaim.Should().NotBeNull();

            var userIdClaim = claims.FindFirst(ClaimTypes.NameIdentifier);
            userIdClaim.Should().NotBeNull();
            userIdClaim.Value.Should().Be(userId);
        }
    }
}