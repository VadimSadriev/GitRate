using System.Threading.Tasks;
using Auth.Application.Handlers;
using GitRate.Auth.Domain;
using GitRate.Auth.UnitTests.Base;
using GitRate.Common.Authentication;
using GitRate.Common.Identity.Types;
using GitRate.Common.Time;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace GitRate.Auth.UnitTests.CommandHandlers
{
    public class RefreshJwtCommandHandlerTests : TestBase
    {
        private readonly RefreshJwtCommandHandler _sut;
        private readonly Mock<IUserManager> _userManagerMock = new Mock<IUserManager>();
        private readonly Mock<IJwtService> _jwtServiceMock = new Mock<IJwtService>();
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public RefreshJwtCommandHandlerTests()
        {
            _sut = new RefreshJwtCommandHandler(_userManagerMock.Object, _jwtServiceMock.Object, new TimeProvider());
        }

        [Fact]
        public async Task Should_Refresh()
        {
            // arrange 
            await FillMockData();
            
            
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