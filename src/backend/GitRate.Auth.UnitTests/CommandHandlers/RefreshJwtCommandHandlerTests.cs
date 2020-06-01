using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Auth.Application.Commands;
using Auth.Application.Handlers;
using GitRate.Auth.Domain;
using GitRate.Auth.UnitTests.Base;
using GitRate.Common.Authentication;
using GitRate.Common.Identity.Types;
using GitRate.Common.Time;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit;

namespace GitRate.Auth.UnitTests.CommandHandlers
{
    public class RefreshJwtCommandHandlerTests : TestBase
    {
        private readonly RefreshJwtCommandHandler _sut;
        private readonly Mock<IUserManager> _userManagerMock = new Mock<IUserManager>();
        private readonly IJwtService _jwtService;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        private readonly Mock<ILogger<JwtService>> _jwtServiceLoggerMock = new Mock<ILogger<JwtService>>();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly Mock<IOptions<JwtOptions>> _jwtOptionsMock = new Mock<IOptions<JwtOptions>>();
        private string _secretKey = "YouWillNeverGuessMySecretKey";
        private string _audience = "GitRate";
        private string _issuer = "GitRate";

        public RefreshJwtCommandHandlerTests()
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey))
            };

            var timeProvider = new TimeProvider();

            _jwtService = new JwtService(timeProvider, _jwtOptionsMock.Object, tokenValidationParameters, _jwtServiceLoggerMock.Object);

            _sut = new RefreshJwtCommandHandler(_userManagerMock.Object, _jwtService, timeProvider);
        }

        [Fact(Skip = "Cant figure out how to test it properly")]
        public async Task Should_Refresh()
        {
            // arrange 
            await FillMockData();

            var user = await _context.Users.FirstAsync();

            var jwt = _jwtService.Create(user.Id);

            var refreshToken = new RefreshToken
            {
                Jti = jwt.Jti,
                UserId = user.Id,
                CreateDate = DateTimeOffset.UtcNow,
                ExpireDate = DateTimeOffset.UtcNow.AddMilliseconds(1)
            };

            _context.RefreshTokens.Add(refreshToken);

            await _context.SaveChangesAsync();

            _jwtOptionsMock.Setup(x => x.Value)
                .Returns(new JwtOptions
                {
                    Audience = _audience,
                    Issuer = _issuer,
                    SecretKey = _secretKey,
                    Expires = 1
                });

            var command = new RefreshJwtCommand
            {
                Jwt = jwt.Token,
                RefreshToken = refreshToken.Id
            };

            // act
            var result = await _sut.Handle(command, _cancellationTokenSource.Token);
        }

        private async Task FillMockData()
        {
            var user = new User
            {
                UserName = "Alice",
                Email = "Alice@gmail.com",
                NormalizedEmail = "ALICE@GMAIL.COM",
                NormalizedUserName = "ALICE"
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, "Alice12345");

            _context.Add(user);

            await _context.SaveChangesAsync();
        }
    }
}